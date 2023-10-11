using ExceptionLibrary.Exceptions;

namespace Identity.Application.Exceptions;

public class TokenNotFoundByIdException : NotFoundByIdException
{
    private const string Type = "Token";

    public TokenNotFoundByIdException(string? id = null)
        : base(Type, id)
    {
    }
}