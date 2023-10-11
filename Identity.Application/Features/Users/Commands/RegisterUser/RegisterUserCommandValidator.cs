using FluentValidation;
using Identity.Application.Validators;

namespace Identity.Application.Features.Users.Commands.RegisterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator(
        PasswordValidator passwordValidator,
        AgeValidator<RegisterUserCommand> ageValidator)
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).SetValidator(passwordValidator);
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Age).SetValidator(ageValidator);
    }
}