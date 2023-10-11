using ExceptionLibrary.Exceptions;

namespace Identity.Application.Exceptions;

public class PasswordNotEqualException : ClientInputException
{
    public PasswordNotEqualException()
        : base("Password doesn't much.")
    {
    }
}