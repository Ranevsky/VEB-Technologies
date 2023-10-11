namespace ExceptionLibrary.Exceptions;

public class NotFoundByEmailException : NotFoundByParameterException
{
    private const string ParameterName = "Email";

    public NotFoundByEmailException(string type, string? email)
        : base(type, ParameterName, email)
    {
    }
}