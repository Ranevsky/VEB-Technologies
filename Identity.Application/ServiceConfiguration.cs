using System.Reflection;
using FluentValidation;
using FluentValidationLibrary.Validators.Id;
using Identity.Application.Models;
using Identity.Application.Validators;
using Identity.Application.Validators.Interfaces;
using MediatR;
using MediatrFluentValidatorBehaviorLibrary.Behaviors;
using Microsoft.Extensions.DependencyInjection;
using ServiceLibrary.Extensions;

namespace Identity.Application;

public static class ServiceConfiguration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var asm = Assembly.GetExecutingAssembly();

        AddMapper(services, asm);
        AddMediatr(services, asm);
        AddFluentValidator(services, asm);
        AddRestrictions(services);
        
        return services;
    }

    private static void AddMapper(IServiceCollection services, Assembly asm)
    {
        services.AddAutoMapper(asm);
    }

    private static void AddMediatr(IServiceCollection services, Assembly asm)
    {
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(asm));
    }

    private static void AddFluentValidator(IServiceCollection services, Assembly asm)
    {
        services.AddValidatorsFromAssembly(asm, filter: opt => opt.ValidatorType != typeof(IdValidator));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddScoped(typeof(IPasswordSymbolValidator<string>), _ => new LengthSymbolValidator<string>(6, 64));
        services.AddScoped(typeof(IPasswordSymbolValidator<string>), _ => new SpecSymbolValidator<string>());
        services.AddScoped(typeof(IPasswordSymbolValidator<string>), _ => new UppercaseSymbolValidator<string>());
        services.AddScoped(typeof(IPasswordSymbolValidator<string>), _ => new LowercaseSymbolValidator<string>());
        services.AddScoped(typeof(AgeValidator<>));
    }

    private static void AddRestrictions(IServiceCollection services)
    {
        services.AddFromConfiguration<UserRestrictions>("Restrictions:User", ServiceLifetime.Scoped);
        services.AddFromConfiguration<CatalogRestrictions>("Restrictions:Catalog", ServiceLifetime.Scoped);
    }
}