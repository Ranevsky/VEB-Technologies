using ExceptionLibrary.Exceptions;

namespace Identity.Application.Exceptions;

public class RoleNotFoundByIdException : NotFoundByIdException
{
    private const string TypeName = "Role";
    
    public RoleNotFoundByIdException(string? id = null)
        : base(TypeName, id)
    {
    }
}