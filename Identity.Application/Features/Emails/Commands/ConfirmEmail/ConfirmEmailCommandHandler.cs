using Identity.Application.Repositories.Interfaces;
using MediatR;

namespace Identity.Application.Features.Emails.Commands.ConfirmEmail;

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Unit>
{
    private readonly IEmailRepository _emailRepository;

    public ConfirmEmailCommandHandler(IEmailRepository emailRepository)
    {
        _emailRepository = emailRepository;
    }

    public async Task<Unit> Handle(
        ConfirmEmailCommand request,
        CancellationToken cancellationToken)
    {
        await _emailRepository.ConfirmEmailAsync(request.EmailId, request.ConfirmKey, cancellationToken);

        return Unit.Value;
    }
}