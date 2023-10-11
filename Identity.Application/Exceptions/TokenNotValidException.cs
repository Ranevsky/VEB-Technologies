using ExceptionLibrary.Exceptions;

namespace Identity.Application.Exceptions;

public class TokenNotValidException : ClientInputException
{
    public TokenNotValidException()
        : base("Token is invalid.")
    {
    }
}