using Microsoft.Extensions.DependencyInjection;
using ResultLibrary.AspNetCore.Configure;

namespace ResultLibrary.AspNetCore.Extensions;

public static class MvcBuilderExtension
{
    public static IMvcBuilder ConfigureInvalidModelStateResponseFactory(this IMvcBuilder mvcBuilder)
    {
        return mvcBuilder.ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = InvalidModelStateResponseFactory.DefaultAspValidationResult;
        });
    }
}