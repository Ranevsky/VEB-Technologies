using LoggerLibrary.Services.Interfaces;
using MassTransit;

namespace LoggerLibrary.MassTransit;

public class TraceIdHeaderSetterFilter<T> : IFilter<PublishContext<T>>
    where T : class
{
    private readonly ITraceIdManager _traceIdManager;

    public TraceIdHeaderSetterFilter(ITraceIdManager traceIdManager)
    {
        _traceIdManager = traceIdManager;
    }

    public async Task Send(PublishContext<T> context, IPipe<PublishContext<T>> next)
    {
        context.Headers.Set(HeadersConst.TraceId, _traceIdManager.TraceId);

        await next.Send(context);
    }

    public void Probe(ProbeContext context)
    {
    }
}