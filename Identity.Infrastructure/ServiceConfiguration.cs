using FluentValidation;
using FluentValidationLibrary.Validators.Id;
using Identity.Application;
using Identity.Application.Models;
using Identity.Application.Repositories.Interfaces;
using Identity.Application.Services;
using Identity.Application.Services.Interfaces;
using Identity.Domain;
using Identity.Infrastructure.Contexts;
using Identity.Infrastructure.Repositories;
using Identity.Infrastructure.Services;
using LoggerLibrary.MassTransit.RabbitMq.Extensions;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceLibrary.Extensions;

namespace Identity.Infrastructure;

public static class ServiceConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplication();
        AddTokenService(services);
        AddPasswordManager(services);
        AddContexts(services, configuration);
        AddRepositories(services);
        AddHealthChecks(services);
        AddIdValidator(services);
        services.AddScoped<IRandomGeneratorService, RandomGeneratorService>();
        services.AddScoped<IEmailSenderService, EmailSenderService>();
        AddMassTransit(services, configuration);

        return services;
    }

    private static void AddIdValidator(IServiceCollection services)
    {
        services.AddScoped<IdValidator, GuidIdValidator>();
    }

    private static void AddTokenService(IServiceCollection services)
    {
        services.AddScoped<ITokenService<UserClaimModel>, TokenService<UserClaimModel>>();
        services.AddFromConfiguration<TokenSettings>("TokenSettings:JwtSettings", ServiceLifetime.Scoped);
        services.AddFromConfiguration<RefreshTokenSettings>(
            "TokenSettings:RefreshTokenSettings",
            ServiceLifetime.Scoped);
    }

    private static void AddPasswordManager(IServiceCollection services)
    {
        services.AddSingleton<IClaimManager<UserClaimModel>, UserClaimManager>();

        services.AddScoped<DefaultHashSettings>(serviceProvider =>
        {
            var cfg = serviceProvider.GetRequiredService<IConfiguration>();
            var defaultArgonSettings = cfg.GetRequiredSection("ArgonSettings").Get<DefaultHashSettings>();

            var validator = serviceProvider.GetRequiredService<ArgonValidation>();
            validator.ValidateAndThrow(defaultArgonSettings);

            return defaultArgonSettings;
        });

        services.AddScoped<IPasswordManager, PasswordManager>();
    }

    private static void AddContexts(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("SqlServer");
            options.UseSqlServer(connectionString);
        });
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IEmailRepository, EmailRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IHashRepository, HashRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
    }

    private static void AddHealthChecks(IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationContext>("SQL Server");
    }

    private static void AddMassTransit(IServiceCollection services, IConfiguration configuration)
    {
        var host = configuration.GetRequiredSection("EventBusSettings:HostAddress").Value;
        services.AddMassTransit(busCfg =>
        {
            busCfg.UsingRabbitMq((context, rabbitCfg) =>
            {
                rabbitCfg.Host(host);

                rabbitCfg.ConfigureEndpoints(context);
                rabbitCfg.UseTraceIdFilter(context);
            });
        });
    }
}