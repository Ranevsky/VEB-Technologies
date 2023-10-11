using ExceptionLibrary.Exceptions;

namespace Identity.Application.Exceptions;

public class EmailNotFoundByIdException : NotFoundByIdException
{
    private const string TypeName = "Email";

    public EmailNotFoundByIdException(string? id = null)
        : base(TypeName, id)
    {
    }
}