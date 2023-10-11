using MediatR;

namespace Identity.Application.Features.Tokens.Commands.RevokeUserToken;

public class RevokeUserTokenCommand : IRequest<Unit>
{
    public string TokenId { get; set; } = null!;
    public string UserId { get; set; } = null!;
}