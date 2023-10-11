using FluentValidation;
using Identity.Application.Validators.Interfaces;

namespace Identity.Application.Validators;

public class UppercaseSymbolValidator<T> : IPasswordSymbolValidator<T>
{
    public bool IsValid(ValidationContext<T> context, string value)
    {
        return value.Any(char.IsUpper);
    }

    public string GetDefaultMessageTemplate(string errorCode)
    {
        return "Must contain at least 1 uppercase character.";
    }

    public string Name => "UppercaseSymbolValidator";
}