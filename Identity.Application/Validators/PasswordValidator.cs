using FluentValidation;
using Identity.Application.Validators.Interfaces;

namespace Identity.Application.Validators;

public class PasswordValidator : AbstractValidator<string>
{
    public PasswordValidator(IEnumerable<IPasswordSymbolValidator<string>> passwordSymbolValidators)
    {
        var rule = RuleFor(x => x).NotEmpty();
        passwordSymbolValidators.Aggregate(rule,
            (current, passwordValidator) => current.SetValidator(passwordValidator));
    }
}