using MediatR;

namespace Identity.Application.Features.Emails.Commands.ConfirmEmail;

public class ConfirmEmailCommand : IRequest<Unit>
{
    public string EmailId { get; set; } = null!;
    public string ConfirmKey { get; set; } = null!;
}