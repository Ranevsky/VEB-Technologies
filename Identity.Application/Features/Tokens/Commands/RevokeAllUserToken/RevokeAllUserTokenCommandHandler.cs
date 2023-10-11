using Identity.Application.Repositories.Interfaces;
using MediatR;

namespace Identity.Application.Features.Tokens.Commands.RevokeAllUserToken;

public class RevokeAllUserTokenCommandHandler : IRequestHandler<RevokeAllUserTokenCommand, Unit>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public RevokeAllUserTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository)
    {
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<Unit> Handle(
        RevokeAllUserTokenCommand request,
        CancellationToken cancellationToken)
    {
        await _refreshTokenRepository.RemoveAllAsync(request.UserId, cancellationToken);

        return Unit.Value;
    }
}