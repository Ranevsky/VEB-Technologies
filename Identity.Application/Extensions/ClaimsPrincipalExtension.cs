using System.Security.Claims;
using ExceptionLibrary.Exceptions;

namespace Identity.Application.Extensions;

public static class ClaimsPrincipalExtension
{
    public static string FindFirstValueOrThrow(this ClaimsPrincipal principal, string claimType)
    {
        if (principal == null)
        {
            throw new ArgumentNullException(nameof(principal));
        }

        return principal.FindFirst(claimType)?.Value
               ?? throw new ClaimNotFoundException(claimType);
    }
}