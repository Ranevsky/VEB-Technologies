using LoggerLibrary.Services.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace LoggerLibrary.MassTransit;

public class TraceIdChangerFilter<T> : IFilter<ConsumeContext<T>>
    where T : class
{
    private readonly ITraceIdManager _traceIdManager;
    private readonly ILogger<TraceIdChangerFilter<T>> _logger;

    public TraceIdChangerFilter(
        ITraceIdManager traceIdManager,
        ILogger<TraceIdChangerFilter<T>> logger)
    {
        _traceIdManager = traceIdManager;
        _logger = logger;
    }

    public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
    {
        var traceId = context.Headers.Get<string>(HeadersConst.TraceId);

        if (traceId is not null)
        {
            _traceIdManager.TraceId = traceId;
        }
        else
        {
            _logger.LogWarning("The trace ID must not be null");
        }

        await next.Send(context);
    }

    public void Probe(ProbeContext context)
    {
    }
}