using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetKubernetes.Dtos.UserDto;
using NetKubernetes.Middleware;
using NetKubernetes.Models;
using NetKubernetes.Token;

namespace NetKubernetes.Data.Users;

public class UserRepository(AppDbContext context, UserManager<User> userManager, 
    SignInManager<User> signInManager, IJwtGenerator jwtGenerator, IUserSession userSession): IUserRepository
{
    private UserResponseDto TransformUserToUserDto(User user)
    {
        return new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Lastname = user.Lastname,
            Phone = user.Phone,
            Email = user.Email,
            Username = user.UserName,
            Token = jwtGenerator.CreateToken(user)
        };
    }
    
    public async Task<UserResponseDto> GetUser()
    {
        var user = await userManager.FindByNameAsync(userSession.GetUserSession());
        if (user == null)
        {
            throw new MiddlewareException(
                HttpStatusCode.Unauthorized,
                new { message = "Token user not found on database" });
        }
        return TransformUserToUserDto(user);
    }

    public async Task<UserResponseDto> Login(UserLoginRequestDto request)
    {
        var user = await userManager.FindByEmailAsync(request.Email!);
        if (user is null)
        {
            throw new MiddlewareException(
                HttpStatusCode.Unauthorized,
                new { message = "User email not exist on database" });
        }
        var result = await signInManager.CheckPasswordSignInAsync(user, request.Password!, false);
        if (result.Succeeded)
            return TransformUserToUserDto(user);
        
        throw new MiddlewareException(
            HttpStatusCode.Unauthorized, 
            new { message = "Incorrect email or password" });
    }

    public async Task<UserResponseDto> Register(UserRegisterRequestDto request)
    {
        var emailExists = await context.Users.Where(u => u.Email == request.Email).AnyAsync();
        if (emailExists)
        {
            throw new MiddlewareException(
                HttpStatusCode.BadRequest,
                new { message = "Email already exists" });
        }
        
        var usernameExists = await context.Users.Where(u => u.UserName == request.Username).AnyAsync();
        if (usernameExists)
        {
            throw new MiddlewareException(
                HttpStatusCode.BadRequest,
                new { message = "Username already exists" });
        }
        
        var user = new User
        {
            Name = request.Name,
            Lastname = request.Lastname,
            Phone = request.Phone,
            Email = request.Email,
            UserName = request.Username,
        };
        
        var result = await userManager.CreateAsync(user, request.Password!);
        if (result.Succeeded)
            return TransformUserToUserDto(user);

        throw new Exception("Oops. User could not be registered");
    }
}