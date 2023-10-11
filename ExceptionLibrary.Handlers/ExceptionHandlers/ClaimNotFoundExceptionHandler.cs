using ExceptionLibrary.Exceptions;
using ResultLibrary.Results.Error;

namespace ExceptionLibrary.Handlers.ExceptionHandlers;

public class ClaimNotFoundExceptionHandler : ExceptionHandler<ClaimNotFoundException, ClaimNotFoundErrorResult>
{
    protected override ClaimNotFoundErrorResult Handle(ClaimNotFoundException exception)
    {
        return new ClaimNotFoundErrorResult("Claim not found.");
    }
}