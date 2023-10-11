namespace Identity.Application.Services.Interfaces;

public interface ITokenService<TClaimModel>
{
    RefreshToken CreateRefreshToken();
    string CreateAccessToken(TClaimModel claimModel);
    TClaimModel GetClaimModel(string accessToken);
}