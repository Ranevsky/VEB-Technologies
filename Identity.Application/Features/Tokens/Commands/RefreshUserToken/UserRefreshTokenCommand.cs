using Identity.Application.Models;
using MediatR;

namespace Identity.Application.Features.Tokens.Commands.RefreshUserToken;

public class UserRefreshTokenCommand : IRequest<TokenViewModel>
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}