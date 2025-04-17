using System.Security.Claims;

namespace NetKubernetes.Token;

public class UserSession(IHttpContextAccessor httpContextAccessor): IUserSession
{
    public string GetUserSession()
    {
        var username = httpContextAccessor
            .HttpContext!
            .User
            .Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        return username!;
    }
}