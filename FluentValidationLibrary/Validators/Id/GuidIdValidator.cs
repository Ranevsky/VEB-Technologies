using FluentValidation;
using FluentValidationLibrary.Extensions;

namespace FluentValidationLibrary.Validators.Id;

public class GuidIdValidator : IdValidator
{
    public GuidIdValidator()
    {
        RuleBuilder
            .Matches("^[0-9A-Fa-f]{8}-([0-9A-Fa-f]{4}-){3}[0-9A-Fa-f]{12}$")
            .WithMessage("Must be in the format 'dddddddd-dddd-dddd-dddd-dddddddddddd', where 'd' is a hex number.")
            .LengthWithoutPropertyName(36);
    }
}