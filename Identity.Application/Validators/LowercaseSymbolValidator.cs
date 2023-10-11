using FluentValidation;
using Identity.Application.Validators.Interfaces;

namespace Identity.Application.Validators;

public class LowercaseSymbolValidator<T> : IPasswordSymbolValidator<T>
{
    public bool IsValid(ValidationContext<T> context, string value)
    {
        return value.Any(char.IsLower);
    }

    public string GetDefaultMessageTemplate(string errorCode)
    {
        return "Must contain at least 1 lowercase character.";
    }

    public string Name => "LowercaseSymbolValidator";
}