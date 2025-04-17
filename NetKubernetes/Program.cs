using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Internal;
using Microsoft.IdentityModel.Tokens;
using NetKubernetes.Data;
using NetKubernetes.Data.Estates;
using NetKubernetes.Data.Users;
using NetKubernetes.Middleware;
using NetKubernetes.Models;
using NetKubernetes.Profiles;
using NetKubernetes.Token;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.LogTo(Console.WriteLine,
        new[] { DbLoggerCategory.Database.Command.Name },
        LogLevel.Information).EnableSensitiveDataLogging();
    
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection"));
});
builder.Services.AddScoped<IEstateRepository, EstateRepository>();

// Add services to the container.

// RESTRICT CONTROLLERS WITH AUTHORIZATION
builder.Services.AddControllers(opt =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// MAPPER CONFIGURATION
var mapperConfig = new MapperConfiguration(config =>
{
    config.AddProfile(new EstateProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// JWT AND SECURITY CONFIGURATION 
var builderSecurity = builder.Services.AddIdentityCore<User>();
var identityBuilder = new IdentityBuilder(builderSecurity.UserType, builder.Services);
identityBuilder.AddEntityFrameworkStores<AppDbContext>();
identityBuilder.AddSignInManager<SignInManager<User>>();
builder.Services.AddSingleton<ISystemClock, SystemClock>();
builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();
builder.Services.AddScoped<IUserSession, UserSession>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("My secret word"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateAudience = false,
            ValidateIssuer = false
        };
    });

// CORS (ALLOW ALL)
builder.Services.AddCors(o => o.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ManagerMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
