using FluentValidation;
using FluentValidationLibrary.Extensions;

namespace FluentValidationLibrary.Validators.Id;

public class IdValidator : AbstractValidator<string?>
{
    protected readonly IRuleBuilder<string?, string?> RuleBuilder;

    public IdValidator()
    {
        RuleBuilder = RuleFor(x => x)
            .NotEmptyWithoutPropertyName()
            .OverridePropertyName(string.Empty);
    }
}