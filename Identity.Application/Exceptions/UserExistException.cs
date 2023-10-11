using ExceptionLibrary.Exceptions;

namespace Identity.Application.Exceptions;

public class UserModelExistByEmailException : ModelExistException
{
    private const string TypeName = "Email";
 
    public UserModelExistByEmailException()
        : base(TypeName)
    {
    }
}