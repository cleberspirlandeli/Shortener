using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shortener.Consumer.API.Consumers;
using Shortener.Consumer.API.Definitions;

namespace Shortener.Consumer.API.Configurations
{
    public static class MassTransitConfig
    {
        public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<UrlConsumer>(typeof(UrlConsumerDefinition));

                x.SetKebabCaseEndpointNameFormatter();

                x.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.ConfigureEndpoints(ctx);
                    cfg.Host("amqp://guest:guest@localhost:5672");

                    cfg.ReceiveEndpoint("queue-url-shorten", c =>
                    {
                        c.ConfigureConsumer<UrlConsumer>(ctx);
                    });

                });
            });

            var provider = services.BuildServiceProvider();

            var busControl = provider.GetRequiredService<IBusControl>();

            busControl.StartAsync();

            return services;
        }

    }
}
