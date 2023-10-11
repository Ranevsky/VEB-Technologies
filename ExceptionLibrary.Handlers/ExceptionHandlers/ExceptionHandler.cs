using ResultLibrary.Results.Error;

namespace ExceptionLibrary.Handlers.ExceptionHandlers;

public abstract class ExceptionHandler
{
    public abstract object? Handle(Exception exception);
}

public abstract class ExceptionHandler<TException, TResult> : ExceptionHandler
    where TException : Exception
    where TResult : ErrorResult
{
    protected abstract TResult? Handle(TException exception);

    public override object? Handle(Exception exception)
    {
        return Handle((TException)exception);
    }
}