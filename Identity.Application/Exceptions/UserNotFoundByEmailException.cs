using ExceptionLibrary.Exceptions;

namespace Identity.Application.Exceptions;

public class UserNotFoundByEmailException : NotFoundByEmailException
{
    private const string Type = "User";

    public UserNotFoundByEmailException(string? email = null)
        : base(Type, email)
    {
    }
}