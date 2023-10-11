using MediatR;

namespace Identity.Application.Features.Tokens.Commands.RevokeAllUserToken;

public class RevokeAllUserTokenCommand : IRequest<Unit>
{
    public string UserId { get; set; } = null!;
}