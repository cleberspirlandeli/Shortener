using GreenPipes;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using Shortener.Consumer.API.Consumers;

namespace Shortener.Consumer.API.Definitions
{
    public class UrlConsumerDefinition : ConsumerDefinition<UrlConsumer>
    {
        public UrlConsumerDefinition()
        {
            // override the default endpoint name, for whatever reason
            // override the default endpoint name, for whatever reason
            EndpointName = "queue-url-shorten";

            // limit the number of messages consumed concurrently
            // this applies to the consumer only, not the endpoint
            ConcurrentMessageLimit = 10;
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<UrlConsumer> consumerConfigurator)
        {
            endpointConfigurator.UseMessageRetry(r => r.Interval(5, 1000));
            endpointConfigurator.UseInMemoryOutbox();
        }
    }
}
