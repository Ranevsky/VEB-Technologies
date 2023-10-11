using ExceptionLibrary.Exceptions;

namespace Identity.Application.Exceptions;

public class UserNotFoundByIdException : NotFoundByIdException
{
    private const string Type = "User";

    public UserNotFoundByIdException(string? id = null)
        : base(Type, id)
    {
    }
}