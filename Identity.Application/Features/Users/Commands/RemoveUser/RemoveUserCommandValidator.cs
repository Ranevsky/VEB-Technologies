using FluentValidation;
using FluentValidationLibrary.Validators.Id;

namespace Identity.Application.Features.Users.Commands.RemoveUser;

public class RemoveUserCommandValidator : AbstractValidator<RemoveUserCommand>
{
    public RemoveUserCommandValidator(IdValidator idValidator)
    {
        RuleFor(x => x.Id).SetValidator(idValidator);
    }
}