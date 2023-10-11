using LoggerLibrary.Services.Interfaces;

namespace LoggerLibrary.Services;

public class GuidTraceIdManager : ITraceIdManager
{
    public GuidTraceIdManager()
    {
        TraceId = Guid.NewGuid().ToString();
    }

    public string TraceId { get; set; }
}