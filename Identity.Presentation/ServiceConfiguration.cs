using System.Reflection;
using System.Text;
using ExceptionLibrary.Exceptions;
using ExceptionLibrary.Handlers.AspNetCore.Extensions;
using ExceptionLibrary.Handlers.ExceptionHandlers;
using Identity.Application;
using Identity.Application.Exceptions;
using Identity.Infrastructure;
using Identity.Presentation.ErrorResult;
using Identity.Presentation.ExceptionHandlers;
using LoggerLibrary.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ResultLibrary.AspNetCore.Extensions;
using ResultLibrary.Results.Error;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;

namespace Identity.Presentation;

public static class ServiceConfiguration
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        var asm = Assembly.GetExecutingAssembly();

        services.AddTraceIdService();
        services.AddInfrastructure(configuration);
        AddSwagger(services, asm);
        AddAuthentication(services, configuration);
        AddMapper(services, asm);
        AddExceptionHandlers(services);

        services.AddControllers().ConfigureInvalidModelStateResponseFactory();
        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
        });
        return services;
    }

    private static void AddSwagger(IServiceCollection services, Assembly asm)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddEnumsWithValuesFixFilters();

            var xmlFilePath = Path.Combine(AppContext.BaseDirectory, $"{asm.GetName().Name}.xml");
            options.IncludeXmlComments(xmlFilePath);

            options.SwaggerDoc("v1", new OpenApiInfo { Title = "My Web API", Version = "v1" });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Scheme = "Bearer",
                Description =
                    "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer",
                        },
                    },
                    Array.Empty<string>()
                },
            });
        });
    }

    private static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        var tokenSettings = configuration.GetRequiredSection("TokenSettings:JwtSettings").Get<TokenSettings>();
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = tokenSettings.Issuer,

            ValidateAudience = true,
            ValidAudience = tokenSettings.Audience,

            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromSeconds(1),

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenSettings.Key)),
        };


        services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParameters;
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        context.HandleResponse();

                        var description = context.ErrorDescription ?? context.AuthenticateFailure switch
                        {
                            SecurityTokenValidationException exception => exception.Message,
                            _ => null,
                        };
                        if (description is not null)
                        {
                            description += ".";
                        }

                        var response = context.Response;
                        var result = new TokenValidationErrorResult(description)
                        {
                            TraceId = response.HttpContext
                                .RequestServices
                                .GetRequiredService<ITraceIdManager>()
                                .TraceId,
                        };

                        response.StatusCode = result.StatusCode;

                        await response.WriteAsJsonAsync((object)result);
                    },
                };
            });
    }

    private static void AddMapper(IServiceCollection services, Assembly asm)
    {
        services.AddAutoMapper(asm);
    }

    private static void AddExceptionHandlers(IServiceCollection services)
    {
        services.AddExceptionHandler<Exception, UnhandledExceptionExceptionHandler>();
        services.AddExceptionHandler<IncorrectException, IncorrectErrorResult, IncorrectExceptionHandler>();
        services.AddExceptionHandler<ModelExistException, ModelExistErrorResult, ModelExistExceptionHandler>();
        services.AddExceptionHandler<NotFoundException, NotFoundErrorResult, NotFoundExceptionHandler>();
        services.AddExceptionHandler<ValidationException, ValidationErrorResult, ValidationExceptionHandler>();
        services.AddExceptionHandler<EmptyUpdateException, EmptyUpdateErrorResult, EmptyUpdateExceptionHandler>();
        services
            .AddExceptionHandler<PasswordNotEqualException, PasswordNotEqualErrorResult,
                PasswordNotEqualExceptionHandler>();
        services
            .AddExceptionHandler<UserModelExistByEmailException, UserExistByEmailErrorResult,
                UserExistByEmailExceptionHandler>();
        services.AddExceptionHandler<TokenNotValidException, TokenValidationErrorResult, TokenNotValidExceptionHandler>();
        services
            .AddExceptionHandler<EmailNotConfirmedException, EmailNotConfirmedErrorResult,
                EmailNotConfirmExceptionHandler>();
        services.AddExceptionHandler<ClaimNotFoundException, ClaimNotFoundErrorResult, ClaimNotFoundExceptionHandler>();
    }
}