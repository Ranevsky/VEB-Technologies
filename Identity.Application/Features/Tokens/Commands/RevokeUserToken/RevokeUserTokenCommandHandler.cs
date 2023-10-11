using Identity.Application.Repositories.Interfaces;
using MediatR;

namespace Identity.Application.Features.Tokens.Commands.RevokeUserToken;

public class RevokeUserTokenCommandHandler : IRequestHandler<RevokeUserTokenCommand, Unit>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public RevokeUserTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository)
    {
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<Unit> Handle(
        RevokeUserTokenCommand request,
        CancellationToken cancellationToken)
    {
        await _refreshTokenRepository.RemoveAsync(request.UserId, request.UserId, cancellationToken);

        return Unit.Value;
    }
}