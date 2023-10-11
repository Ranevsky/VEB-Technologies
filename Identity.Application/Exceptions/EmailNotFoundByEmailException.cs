using ExceptionLibrary.Exceptions;

namespace Identity.Application.Exceptions;

public class EmailNotFoundByEmailException : NotFoundByEmailException
{
    private const string TypeName = "Email";

    public EmailNotFoundByEmailException(string? email)
        : base(TypeName, email)
    {
    }
}