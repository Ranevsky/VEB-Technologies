namespace ExceptionLibrary.Exceptions;

public class ValidationException : ClientInputException
{
    public readonly IDictionary<string, string[]> Errors;

    public ValidationException(IDictionary<string, string[]> errors)
        : base("Validation error.")
    {
        Errors = errors;
    }
}