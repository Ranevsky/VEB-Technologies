using FluentValidation;
using Identity.Application.Validators;

namespace Identity.Application.Features.Users.Queries.LoginUser;

public class LoginUserQueryValidator : AbstractValidator<LoginUserQuery>
{
    public LoginUserQueryValidator(PasswordValidator passwordValidator)
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).SetValidator(passwordValidator);
    }
}