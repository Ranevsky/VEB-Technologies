namespace ExceptionLibrary.Exceptions;

public abstract class NotFoundException : ClientInputException
{
    protected NotFoundException(string message)
        : base(message)
    {
    }
}