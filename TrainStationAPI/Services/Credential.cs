using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TrainStationAPI.Model;

namespace TrainStationAPI.Services
{
    public class Credential
    {
        readonly IConfiguration _config;
        readonly UserManager<UserModel> _userManager;
        public Credential(IConfiguration _config, UserManager<UserModel> _userManager)
        {
            this._config = _config;
            this._userManager = _userManager;
        }
        private IConfigurationSection GetSection()
        {
            return _config.GetSection("JwtSettings");
        }

        public SigningCredentials GetSigningCredentials()
        {
            var _jwtSettings = GetSection();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.GetSection("securityKey").Value);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        public JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claim)
        {
            var _jwtSettings = GetSection();
            var tokenOptions = new JwtSecurityToken(
            issuer: _jwtSettings.GetSection("validIssuer").Value,
            audience: _jwtSettings.GetSection("validAudience").Value,
            claims: claim,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings.GetSection("expiryInMinutes").Value)),
            signingCredentials: signingCredentials);
            return tokenOptions;
        }
        public async Task<List<Claim>> GetClaims(UserModel user)
        {
            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.Name, user.Email)
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
    }
}
