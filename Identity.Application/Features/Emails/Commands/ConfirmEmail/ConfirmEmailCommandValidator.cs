using FluentValidation;
using FluentValidationLibrary.Validators.Id;

namespace Identity.Application.Features.Emails.Commands.ConfirmEmail;

public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
{
    public ConfirmEmailCommandValidator(IdValidator idValidator)
    {
        RuleFor(x => x.EmailId).SetValidator(idValidator);
        RuleFor(x => x.ConfirmKey).NotEmpty();
    }
}