namespace NetKubernetes.Dto.UserDto;

public class UserRegisterRequestDto
{
    public string? Name { get; set; }
    public string? Lastname { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
}