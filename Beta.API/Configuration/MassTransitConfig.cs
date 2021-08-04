using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Beta.API.Configuration
{
    public static class MassTransitConfig
    {
        public static IServiceCollection AddMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(bus =>
            {
                bus.UsingRabbitMq((context, config) =>
                {
                    config.Host(configuration.GetConnectionString("RabbitMq"));
                });
            });
            services.AddMassTransitHostedService();

            return services;
        }
    }
}
