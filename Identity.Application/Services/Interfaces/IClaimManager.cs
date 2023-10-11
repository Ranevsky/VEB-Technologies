using System.Security.Claims;

namespace Identity.Application.Services.Interfaces;

public interface IClaimManager<TClaimModel>
{
    IList<Claim> ToClaims(TClaimModel model);
    TClaimModel ToClaimModel(IEnumerable<Claim> claims);
}