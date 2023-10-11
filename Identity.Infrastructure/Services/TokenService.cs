using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Identity.Application;
using Identity.Application.Exceptions;
using Identity.Application.Models;
using Identity.Application.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Infrastructure.Services;

internal class TokenService<TClaimModel> : ITokenService<TClaimModel>
    where TClaimModel : ClaimModel
{
    private const string SecurityAlgorithm = SecurityAlgorithms.HmacSha256;
    private readonly IClaimManager<TClaimModel> _claimManager;

    private readonly IRandomGeneratorService _randomGeneratorService;
    private readonly RefreshTokenSettings _refreshTokenSettings;
    private readonly TokenSettings _tokenSettings;

    public TokenService(
        TokenSettings tokenSettings,
        RefreshTokenSettings refreshTokenSettings,
        IRandomGeneratorService randomGeneratorService,
        IClaimManager<TClaimModel> claimManager)
    {
        _tokenSettings = tokenSettings;
        _refreshTokenSettings = refreshTokenSettings;
        _randomGeneratorService = randomGeneratorService;
        _claimManager = claimManager;
    }

    public string CreateAccessToken(TClaimModel model)
    {
        var claims = _claimManager.ToClaims(model);
        claims.Add(new Claim(Claims.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(Claims.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)));

        var expireTime = DateTime.UtcNow.AddSeconds(_tokenSettings.ExpireTime);

        var token = new JwtSecurityToken(
            _tokenSettings.Issuer,
            _tokenSettings.Audience,
            claims,
            expires: expireTime,
            signingCredentials: CreateSigningCredentials(_tokenSettings.Key));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public RefreshToken CreateRefreshToken()
    {
        var refreshToken = _randomGeneratorService.Generate(_refreshTokenSettings.TokenLength);
        var result = new RefreshToken
        {
            Token = refreshToken,
            ExpireTime = DateTime.UtcNow.AddSeconds(_refreshTokenSettings.ExpireTime),
        };

        return result;
    }

    public TClaimModel GetClaimModel(string accessToken)
    {
        var claimPrincipal = GetClaimsPrincipal(accessToken);
        var claimModel = _claimManager.ToClaimModel(claimPrincipal.Claims);

        return claimModel;
    }

    private ClaimsPrincipal GetClaimsPrincipal(string accessToken)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = _tokenSettings.Issuer,

            ValidateAudience = false,

            ValidateLifetime = false,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_tokenSettings.Key)),
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        ClaimsPrincipal principal;
        SecurityToken? securityToken;

        try
        {
            principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out securityToken);
        }
        catch (ArgumentException)
        {
            throw new TokenNotValidException();
        }

        if (securityToken is not JwtSecurityToken jwtSecurityToken
            || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithm, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new TokenNotValidException();
        }

        return principal;
    }

    private static SigningCredentials CreateSigningCredentials(string key)
    {
        var keyByte = Encoding.UTF8.GetBytes(key);
        var securityKey = new SymmetricSecurityKey(keyByte);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithm);

        return credentials;
    }
}