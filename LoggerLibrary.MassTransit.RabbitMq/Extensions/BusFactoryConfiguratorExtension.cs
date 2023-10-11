using MassTransit;

namespace LoggerLibrary.MassTransit.RabbitMq.Extensions;

public static class BusFactoryConfiguratorExtension
{
    public static void UseTraceIdFilter(this IBusFactoryConfigurator configurator, IServiceProvider provider)
    {
        configurator.UsePublishFilter(typeof(TraceIdHeaderSetterFilter<>), provider);

        configurator.UseConsumeFilter(typeof(TraceIdChangerFilter<>), provider);
        configurator.UseConsumeFilter(typeof(TraceIdLoggerFilter<>), provider);
    }
}