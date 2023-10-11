namespace ExceptionLibrary.Exceptions;

public class ModelExistException : ClientInputException
{
    public ModelExistException(string typeName)
        : base($"{typeName} already exists.")
    {
    }
}