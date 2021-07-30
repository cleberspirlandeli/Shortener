using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shortener.Consumer.API.Consumers;
using Shortener.Services.ApplicationService;
using System;

namespace Shortener.Consumer.API.Configurations
{
    public static class MassTransitConfig
    {
        public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration)
        {


            services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(configuration.GetConnectionString("RabbitMq"));

                cfg.ReceiveEndpoint("queue-url-shorten", e =>
                {
                    e.Consumer<UrlConsumer>(provider);
                });
            }));

            services.AddMassTransitHostedService();

            return services;
        }

    }
}
