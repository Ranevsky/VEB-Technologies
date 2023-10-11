using System.Security.Claims;
using ExceptionLibrary.Exceptions;
using Identity.Application.Models;
using Identity.Application.Services.Interfaces;

namespace Identity.Application.Services;

public class UserClaimManager : IClaimManager<UserClaimModel>
{
    public IList<Claim> ToClaims(UserClaimModel user)
    {
        var claims = new List<Claim>
        {
            new(Claims.Id, user.Id),
            new(Claims.Name, user.Name),
            new(Claims.Email, user.Email),
        };
        claims.AddRange(user.Roles.Select(role => new Claim(Claims.Roles, role)));

        return claims;
    }

    public UserClaimModel ToClaimModel(IEnumerable<Claim> claims)
    {
        claims = claims.ToList();

        var user = new UserClaimModel
        {
            Id = GetClaimValue(claims, Claims.Id),
            Name = GetClaimValue(claims, Claims.Name),
            Email = GetClaimValue(claims, Claims.Email),
            Roles = GetClaimValues(claims, Claims.Roles),
        };
        return user;
    }

    private static string GetClaimValue(IEnumerable<Claim> claims, string claimType)
    {
        return claims.FirstOrDefault(claim => claim.Type == claimType)?.Value
               ?? throw new ClaimNotFoundException(claimType);
    }
    
    private static string[] GetClaimValues(IEnumerable<Claim> claims, string claimType)
    {
        var values = claims.Where(claim => claim.Type == claimType).Select(claim => claim.Value).ToArray();

        if (values.Length < 1)
        {
            throw new ClaimNotFoundException(claimType);
        }

        return values;
    }
}