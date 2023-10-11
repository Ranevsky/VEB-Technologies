using System.Text.Json.Serialization;

namespace ResultLibrary.Results.Error;

public class ErrorResult
{
    public ErrorResult(string title, int statusCode)
    {
        Title = title;
        StatusCode = statusCode;
    }

    public string Title { get; set; }
    public string TraceId { get; set; } = null!;

    [JsonIgnore]
    public int StatusCode { get; set; }
}