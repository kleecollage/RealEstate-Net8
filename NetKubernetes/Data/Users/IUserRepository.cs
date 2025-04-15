using NetKubernetes.Dtos.UserDto;

namespace NetKubernetes.Data.Users;

public interface IUserRepository
{
    Task<UserResponseDto> GetUser();
    Task<UserResponseDto> Login(UserLoginRequestDto request);
    Task<UserResponseDto> Register(UserRegisterRequestDto request);
}