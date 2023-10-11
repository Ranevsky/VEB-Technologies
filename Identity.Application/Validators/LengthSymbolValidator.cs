using FluentValidation.Validators;
using Identity.Application.Validators.Interfaces;

namespace Identity.Application.Validators;

public class LengthSymbolValidator<T> :
    LengthValidator<T>,
    IPasswordSymbolValidator<T>
{
    public LengthSymbolValidator(int min, int max)
        : base(min, max)
    {
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return "Must be between {MinLength} and {MaxLength} characters. You entered {TotalLength} characters.";
    }
}