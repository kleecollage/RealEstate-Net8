using Microsoft.AspNetCore.Identity;
using NetKubernetes.Dtos.UserDto;
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
        return TransformUserToUserDto(user!);
    }

    public async Task<UserResponseDto> Login(UserLoginRequestDto request)
    {
        var user = await userManager.FindByEmailAsync(request.Email!);
        await signInManager.CheckPasswordSignInAsync(user!, request.Password!, false);
        return TransformUserToUserDto(user!);
    }

    public async Task<UserResponseDto> Register(UserRegisterRequestDto request)
    {
        var user = new User
        {
            Name = request.Name,
            Lastname = request.Lastname,
            Phone = request.Phone,
            Email = request.Email,
            UserName = request.Username,
        };
        
        await userManager.CreateAsync(user, request.Password!);
        return TransformUserToUserDto(user);
    }
}