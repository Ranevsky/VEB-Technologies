using System.Text.Json.Serialization;

namespace ResultLibrary.Results.Error;

public abstract class ServerErrorResult : ErrorResult
{
    protected ServerErrorResult(string? title, int statusCode)
        : base(title ?? "Internal Server Error.", statusCode)
    {
    }

    [JsonIgnore]
    public string? Message { get; set; }
}