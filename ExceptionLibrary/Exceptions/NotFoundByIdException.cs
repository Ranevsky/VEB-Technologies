namespace ExceptionLibrary.Exceptions;

public class NotFoundByIdException : NotFoundByParameterException
{
    private const string ParameterName = "Id";

    public NotFoundByIdException(string type, string? id = null)
        : base(type, ParameterName, id)
    {
    }
}