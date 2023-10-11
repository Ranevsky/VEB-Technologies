namespace ExceptionLibrary.Exceptions;

public abstract class EmptyUpdateException : ClientInputException
{
    protected EmptyUpdateException(string type)
        : base(CreateMessage(type))
    {
    }

    private static string CreateMessage(string type)
    {
        ArgumentValidator.ThrowIfNullOrEmpty(type, nameof(type));

        return $"{type} does not have updatable fields.";
    }
}