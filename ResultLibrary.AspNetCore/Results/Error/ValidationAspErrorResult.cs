using Microsoft.AspNetCore.Mvc.ModelBinding;
using ResultLibrary.Results.Error;

namespace ResultLibrary.AspNetCore.Results.Error;

public class ValidationAspErrorResult : ValidationErrorResult
{
    public ValidationAspErrorResult(ModelStateDictionary modelState)
        : base(GetDictionary(modelState))
    {
    }

    private static IDictionary<string, string[]> GetDictionary(ModelStateDictionary modelState)
    {
        return modelState
            .Where(x => x.Value?.Errors is { Count: > 0 })
            .Select(x => new
            {
                x.Key,
                ErrorMessage = x.Value!.Errors.Select(modelError => modelError.ErrorMessage)
                    .Where(errorMessage => !string.IsNullOrEmpty(errorMessage)).ToArray(),
            })
            .Where(x => x.ErrorMessage.Length > 0)
            .ToDictionary(x => x.Key, x => x.ErrorMessage);
    }
}