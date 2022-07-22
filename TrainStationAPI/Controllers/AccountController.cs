using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TrainStationAPI.Model;
using TrainStationAPI.Model.DTO;
using TrainStationAPI.Services;

namespace TrainStationAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        readonly UserManager<UserModel> _userManager;
        readonly EmailService _emailService;
        readonly SignInManager<UserModel> _signInManager;
        readonly PasswordHasher<UserModel> _passwordHasher;
        readonly Credential _credential;
        public AccountController(UserManager<UserModel> userManager, EmailService emailService, SignInManager<UserModel> signInManager, 
            PasswordHasher<UserModel> passwordHasher, Credential credential)
        {
            _userManager = userManager;
            _emailService = emailService;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
            _credential = credential;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(SignUp newUser)
        {
          try 
          { 
           UserModel user = newUser.Adapt<UserModel>(MappingService.AdminConfig());
            if (newUser.Password.Equals(newUser.ConfirmPassword))
            {
                IdentityResult identity = await _userManager.CreateAsync(user, user.PasswordHash);
                if (identity.Succeeded)
                {
                    string token = await EmailConfirmationToken(user);
                    Mail newMail = new Mail
                    {
                        To = user.Email,
                        Text = $"Verify your Email using this token {token}"
                    };

                    bool isSuccessful = await _emailService.SendSimpleMessage(newMail);
                    if (isSuccessful)
                    {
                        return this.StatusCode(StatusCodes.Status201Created, $"Welcome,{user.UserName} verify your email");
                    }
                    return BadRequest();
                }
                else
                {
                    return BadRequest(identity.Errors);
                }
            }
            return this.StatusCode(StatusCodes.Status400BadRequest, "Password not equal,retype password");
          }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }

        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("emailconfirmation", Name = "EmailConfirmation")]
        public async Task<ActionResult> VerifyEmailToken([FromBody] Token token, string email)
        {
            try
            {
                UserModel currentUser = await _userManager.FindByEmailAsync(email);
                if (currentUser == null)
                {
                    return NotFound("username doesn't exist");
                }
                IdentityResult result = await _userManager.ConfirmEmailAsync(currentUser, token.GeneratedToken);
                if (result.Succeeded)
                {
                    return this.StatusCode(StatusCodes.Status200OK, $"Welcome,{currentUser.UserName} Email has been verified");
                }
                else
                {
                    return this.StatusCode(StatusCodes.Status400BadRequest, "Invalid Token");
                }
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }


        [AllowAnonymous]
        [Produces("application/json")]
        [HttpPost("login")]
        public async Task<ActionResult> Login(Login user)
        {
            try
            {
                UserModel currentUser = await _userManager.FindByEmailAsync(user.Email);
                if (currentUser == null)
                {
                    return NotFound("username or password is not correct");
                }

                //This verfies the user password by using IPasswordHasher interface
                PasswordVerificationResult passwordVerifyResult = _passwordHasher.VerifyHashedPassword(currentUser, currentUser.PasswordHash, user.Password);
                if (passwordVerifyResult.ToString() == "Success")
                {

                    var claim = new Claim(ClaimTypes.Name, $"{user.Email}");
                    List<Claim> claims = new List<Claim> { claim };
                    var identity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);

                   // await _signInManager.SignInWithClaimsAsync(currentUser, null, claims);

                    var signingCredentials = _credential.GetSigningCredentials();
                    var tokenOptions = _credential.GenerateTokenOptions(signingCredentials, claims);
                    var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                    return Ok(token);
                }

                return BadRequest("username or password is not correct");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Produces("application/json")]
        [HttpPost("signout")]
        public async Task<ActionResult> SignOut()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return Ok("Signed out");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }


        [NonAction]
        private async Task<string> EmailConfirmationToken(UserModel newUser)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

        }
    }
}

