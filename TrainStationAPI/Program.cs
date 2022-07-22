using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NHibernate.AspNetCore.Identity;
using NHibernate.NetCore;
using System.Reflection;
using System.Text;
using TrainStationAPI.Model;
using TrainStationAPI.Services;
using IdentityRole = NHibernate.AspNetCore.Identity.IdentityRole;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("TrainStation");
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddSingleton<EmailService>();
builder.Services.AddScoped<Credential>();
builder.Services.AddScoped(x => new FluentNhibernateHelper(connectionString));
builder.Services.AddScoped<ITrainStation<Train>,TrainStationRepository<Train>>();
builder.Services.AddScoped<ITrainInfo, TrainInfoRepository>();
builder.Services.AddScoped<ITrainStation<Station>, TrainStationRepository<Station>>();
builder.Services.AddScoped<ITrainStation<Connection>, TrainStationRepository<Connection>>();

builder.Services.AddHibernate(FluentNhibernateHelper.CreateConfiguration(connectionString));
builder.Services.AddScoped(x => new PasswordHasher<UserModel>());
builder.Services.AddIdentity<UserModel,IdentityRole>().AddHibernateStores().AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.LoginPath = PathString.Empty;
    options.AccessDeniedPath = PathString.Empty;

});
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 6;
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = true;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("trainstatation", new OpenApiInfo()
    {
        Title = "Train Station API",
        Version = "1.0",
        Description = "A Simple Crud operation ",
        Contact = new OpenApiContact()
        {
            Email = "adeolaaderibigbe09@gmail.com",
            Name = "Adeola Aderibigbe",
            Url = new Uri("https://github.com/Adexandria")

        }

    });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

    c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {

        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        In = ParameterLocation.Header,
        BearerFormat = "bearer",
        Description = "Enter Token Only"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                         new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "bearer"
                                }
                            },
                          new string[] {}
                    }

                });
    var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
    //... and tell Swagger to use those XML comments.
    c.IncludeXmlComments(xmlPath);
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
        ValidAudience = jwtSettings.GetSection("validAudience").Value,
        AuthenticationType = "Bearer",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection("securityKey").Value))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/swagger/trainstatation/swagger.json", "Train Station API");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
