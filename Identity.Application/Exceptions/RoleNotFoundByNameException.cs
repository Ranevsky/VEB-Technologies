using ExceptionLibrary.Exceptions;

namespace Identity.Application.Exceptions;

public class RoleNotFoundByNameException : NotFoundByNameException
{
    private const string Type = "Role";
    
    public RoleNotFoundByNameException(string? id = null)
        : base(Type, id)
    {
    }
}