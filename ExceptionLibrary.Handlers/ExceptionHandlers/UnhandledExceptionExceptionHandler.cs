using ResultLibrary.Results.Error;

namespace ExceptionLibrary.Handlers.ExceptionHandlers;

public class UnhandledExceptionExceptionHandler : ExceptionHandler
{
    public override object Handle(Exception exception)
    {
        return new UnhandledExceptionErrorResult();
    }
}