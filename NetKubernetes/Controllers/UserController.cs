using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetKubernetes.Data.Users;
using NetKubernetes.Dto.UserDto;

namespace NetKubernetes.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserRepository repository): ControllerBase
{
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<UserResponseDto>> Login([FromBody] UserLoginRequestDto request)
    {
        return await repository.Login(request);
    }
    
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<UserResponseDto>> Register([FromBody] UserRegisterRequestDto request)
    {
        return await repository.Register(request);
    }
    
    [HttpGet]
    public async Task<ActionResult<UserResponseDto>> ReturnUser()
    {
        return await repository.GetUser();
    }
}