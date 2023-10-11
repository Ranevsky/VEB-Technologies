namespace ExceptionLibrary.Exceptions;

public abstract class IncorrectException : ClientInputException
{
    protected IncorrectException(string details)
    {
        Details = details;
    }

    public string Details { get; set; }
}