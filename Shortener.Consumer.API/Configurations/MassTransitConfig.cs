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

                x.AddConsumer<UrlConsumer>();
                x.AddConsumer<UrlUpdateInfoConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration.GetConnectionString("RabbitMq"));

                    cfg.ReceiveEndpoint("queue-url-shorten", e =>
                    {
                        e.ConfigureConsumer<UrlConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("queue-url-update-info", e =>
                    {
                        e.ConfigureConsumer<UrlUpdateInfoConsumer>(context);
                    });
                });
            });

            services.AddMassTransitHostedService();

            return services;
        }

    }
}
