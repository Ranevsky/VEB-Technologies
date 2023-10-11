using ExceptionLibrary.Exceptions;

namespace ExceptionLibrary;

public static class ArgumentValidator
{
    public static void ThrowIfOutOfRange<TEnum>(TEnum value, string? paramName = null)
        where TEnum : struct, Enum
    {
        if (Enum.IsDefined(value))
        {
            return;
        }

        throw new ArgumentOutOfRangeException(
            paramName,
            value,
            "The value provided is outside the range of valid values for the EnumType enumeration.");
    }

    public static void ThrowIfNullOrEmpty(string? text, string? paramName = null)
    {
        if (!string.IsNullOrWhiteSpace(text))
        {
            return;
        }

        throw new ArgumentNullOrEmptyException(paramName);
    }
}