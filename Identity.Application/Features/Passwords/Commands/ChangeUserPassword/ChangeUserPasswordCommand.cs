using MediatR;

namespace Identity.Application.Features.Passwords.Commands.ChangeUserPassword;

public class ChangeUserPasswordCommand : IRequest<Unit>
{
    public string UserId { get; set; } = null!;
    public string OldPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    public bool NeedLogoutAccounts { get; set; }
}