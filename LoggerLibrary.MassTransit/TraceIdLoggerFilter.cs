using LoggerLibrary.Services.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace LoggerLibrary.MassTransit;

public class TraceIdLoggerFilter<T> : IFilter<ConsumeContext<T>>
    where T : class
{
    private readonly ITraceIdManager _traceIdManager;
    private readonly ILogger<TraceIdLoggerFilter<T>> _logger;

    public TraceIdLoggerFilter(
        ITraceIdManager traceIdManager,
        ILogger<TraceIdLoggerFilter<T>> logger)
    {
        _traceIdManager = traceIdManager;
        _logger = logger;
    }

    public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
    {
        using var _ = _logger.BeginScope(_traceIdManager.TraceId);

        await next.Send(context);
    }

    public void Probe(ProbeContext context)
    {
    }
}