namespace ExceptionLibrary.Exceptions;

public class NotFoundByParameterException : NotFoundException
{
    public NotFoundByParameterException(string type, string parameterName, string? parameterValue)
        : base(CreateMessage(type, parameterName, parameterValue))
    {
    }

    private static string CreateMessage(string type, string parameterName, string? parameterValue)
    {
        ArgumentValidator.ThrowIfNullOrEmpty(type, nameof(type));
        ArgumentValidator.ThrowIfNullOrEmpty(parameterName, nameof(parameterName));

        var message = parameterValue is null
            ? $"{type} not found."
            : $"{type} with {parameterName} = '{parameterValue}' not found.";

        return message;
    }
}