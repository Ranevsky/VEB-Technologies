using FluentValidation;
using FluentValidationLibrary.Validators.Id;
using Identity.Application.Validators;

namespace Identity.Application.Features.Passwords.Commands.ChangeUserPassword;

public class ChangeUserPasswordCommandValidator : AbstractValidator<ChangeUserPasswordCommand>
{
    public ChangeUserPasswordCommandValidator(
        IdValidator idValidator,
        PasswordValidator passwordValidator)
    {
        RuleFor(x => x.UserId).SetValidator(idValidator);
        RuleFor(x => x.OldPassword).SetValidator(passwordValidator);
        RuleFor(x => x.NewPassword).SetValidator(passwordValidator);

        RuleFor(x => x.OldPassword)
            .NotEqual(x => x.NewPassword)
            .WithMessage("New Password must not be equal to Old Password.");
    }
}