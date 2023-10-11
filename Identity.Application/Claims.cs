using System.Security.Claims;

namespace Identity.Application;

public class Claims
{
    public const string Id = "Id";
    public const string Name = "Name";
    public const string Email = "Email";
    public const string Roles = ClaimTypes.Role;

    public const string Jti = "jti";
    public const string Iat = "iat";
}