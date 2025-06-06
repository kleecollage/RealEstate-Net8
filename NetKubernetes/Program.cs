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


/* MSSQL CONNECTION 
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.LogTo(Console.WriteLine,
        new[] { DbLoggerCategory.Database.Command.Name },
        LogLevel.Information).EnableSensitiveDataLogging();
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection"));
});*/

// MYSQL CONNECTION
var connectionString = builder.Configuration.GetConnectionString("MySqlHeroku");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// Add services to the container.
builder.Services.AddScoped<IEstateRepository, EstateRepository>();


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

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
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

// CUSTOM MIDDLEWATE
app.UseMiddleware<ManagerMiddleware>();

app.UseAuthentication();
app.UseCors("corsapp");

app.UseAuthorization();

app.MapControllers();


// SERVICES
using (var environment = app.Services.CreateScope())
{
    var services = environment.ServiceProvider;
    try
    {
        var userManager = services.GetRequiredService<UserManager<User>>();
        var context = services.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();
        await LoadDatabase.InsertData(context, userManager);
    }
    catch (Exception e)
    {
        var logging = services.GetRequiredService<ILogger<Program>>();
        logging.LogError(e, "An error occured during migration");
    }
}

app.Run();







