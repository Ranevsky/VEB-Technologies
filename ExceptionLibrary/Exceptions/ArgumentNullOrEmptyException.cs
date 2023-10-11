namespace ExceptionLibrary.Exceptions;

public class ArgumentNullOrEmptyException : ArgumentException
{
    public ArgumentNullOrEmptyException(string? paramName)
        : base("Value cannot be null or empty.", paramName)
    {
    }
}