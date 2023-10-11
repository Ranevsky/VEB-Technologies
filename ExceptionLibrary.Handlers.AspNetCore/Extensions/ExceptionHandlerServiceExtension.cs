using System.Reflection;
using ExceptionLibrary.Handlers.ExceptionHandlers;
using Microsoft.Extensions.DependencyInjection;
using ResultLibrary.Results.Error;

namespace ExceptionLibrary.Handlers.AspNetCore.Extensions;

public static class ExceptionHandlerServiceExtension
{
    private static Dictionary<Type, ExceptionHandler>? _handlers;

    public static void AddExceptionHandlerFromAssembly(this IServiceCollection services, Assembly assembly)
    {
        TryAddService(services);

        var types = assembly
            .GetTypes()
            .Where(t =>
                t is { IsClass: true, IsAbstract: false, BaseType.IsGenericType: true } &&
                t.BaseType.GetGenericTypeDefinition() == typeof(ExceptionHandler<,>) &&
                t.GetConstructor(Type.EmptyTypes) is not null)
            .Select(handlerType => new
            {
                ExceptionType = handlerType.BaseType!.GenericTypeArguments[0],
                Handler = (ExceptionHandler)Activator.CreateInstance(handlerType)!,
            });

        foreach (var type in types)
        {
            _handlers!.Add(type.ExceptionType, type.Handler);
        }
    }

    public static void AddExceptionHandler<TException, TExceptionHandlerResult, TExceptionHandler>(
        this IServiceCollection services)
        where TException : Exception
        where TExceptionHandlerResult : ErrorResult
        where TExceptionHandler : ExceptionHandler<TException, TExceptionHandlerResult>, new()
    {
        TryAddService(services);

        _handlers!.Add(typeof(TException), new TExceptionHandler());
    }

    public static void AddExceptionHandler<TException, TExceptionHandler>(
        this IServiceCollection services)
        where TException : Exception
        where TExceptionHandler : ExceptionHandler, new()
    {
        TryAddService(services);

        _handlers!.Add(typeof(TException), new TExceptionHandler());
    }

    private static void TryAddService(IServiceCollection services)
    {
        if (_handlers is not null)
        {
            return;
        }

        _handlers = new Dictionary<Type, ExceptionHandler>();
        services.AddSingleton<IDictionary<Type, ExceptionHandler>>(_ => _handlers);
    }
}