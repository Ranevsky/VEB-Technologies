using Identity.Application.Exceptions;
using Identity.Application.Repositories.Interfaces;
using Identity.Application.Services.Interfaces;
using MediatR;

namespace Identity.Application.Features.Passwords.Commands.ChangeUserPassword;

public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, Unit>
{
    private readonly IPasswordManager _passwordManager;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserRepository _userRepository;

    public ChangeUserPasswordCommandHandler(
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IPasswordManager passwordManager)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _passwordManager = passwordManager;
    }

    public async Task<Unit> Handle(
        ChangeUserPasswordCommand request,
        CancellationToken cancellationToken)
    {
        var passwordDb = await _userRepository.GetPasswordAsync(request.UserId, cancellationToken);
        var isEquals = await _passwordManager.IsEqualsAsync(passwordDb, request.OldPassword, cancellationToken);
        if (!isEquals)
        {
            throw new PasswordNotEqualException();
        }

        var newPass = await _passwordManager.CreatePasswordAsync(request.NewPassword, cancellationToken);
        await _userRepository.SetPasswordAsync(request.UserId, newPass, cancellationToken);

        if (request.NeedLogoutAccounts)
        {
            await _refreshTokenRepository.RemoveAllAsync(request.UserId, cancellationToken);
        }

        return Unit.Value;
    }
}