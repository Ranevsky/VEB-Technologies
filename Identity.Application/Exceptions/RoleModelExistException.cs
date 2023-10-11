using ExceptionLibrary.Exceptions;

namespace Identity.Application.Exceptions;

public class RoleModelExistException : ModelExistException
{
    private const string TypeName = "Role";
    
    public RoleModelExistException()
        : base(TypeName)
    {
    }
}