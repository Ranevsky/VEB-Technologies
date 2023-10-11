using FluentValidation;
using Identity.Application.Validators.Interfaces;

namespace Identity.Application.Validators;

public class SpecSymbolValidator<T> : IPasswordSymbolValidator<T>
{
    public bool IsValid(ValidationContext<T> context, string value)
    {
        return value.Any(IsSpecSymbol);

        static bool IsSpecSymbol(char symbol)
        {
            var symbolIndex = (int)symbol;

            return IsBetween(symbolIndex, 32, 47) || // ' ', '!', '#', ...
                   IsBetween(symbolIndex, 58, 64) || // ':', ';', '<', ...
                   IsBetween(symbolIndex, 91, 96) || // '[', '\', ']', ...
                   IsBetween(symbolIndex, 123, 126); // '{', '|', '}', ...

            static bool IsBetween(int num, int a, int b)
            {
                return num >= a && num <= b;
            }
        }
    }

    public string GetDefaultMessageTemplate(string errorCode)
    {
        return "Must contain at least 1 special character.";
    }

    public string Name => "SpecSymbolValidator";
}