namespace ExceptionLibrary.Exceptions;

public class NotFoundByNameException : NotFoundByParameterException
{
    private const string ParameterName = "Name";
    
    public NotFoundByNameException(string type, string? parameterValue = null)
        : base(type, ParameterName, parameterValue)
    {
    }
}