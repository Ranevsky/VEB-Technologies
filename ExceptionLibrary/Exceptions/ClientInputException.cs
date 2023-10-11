namespace ExceptionLibrary.Exceptions;

public abstract class ClientInputException : Exception
{
    protected ClientInputException(string? message = null)
        : base(message)
    {
    }
}