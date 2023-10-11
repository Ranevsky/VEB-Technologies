using FluentValidation;
using FluentValidationLibrary.Extensions;

namespace FluentValidationLibrary.Validators.Id;

public class BsonIdValidator : IdValidator
{
    public BsonIdValidator()
    {
        RuleBuilder
            .Matches(@"\b[0-9A-Fa-f]{24}\b")
            .WithMessage("Must be in the format 'dddddddddddddddddddddddd', where 'd' is a hex number.")
            .LengthWithoutPropertyName(24);
    }
}