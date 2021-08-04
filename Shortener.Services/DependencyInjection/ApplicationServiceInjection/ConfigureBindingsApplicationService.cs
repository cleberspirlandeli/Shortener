using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shortener.Services.ApplicationService;
using Shortener.Services.Cache;
using StackExchange.Redis;

namespace Shortener.Services.DependencyInjection.ApplicationServiceInjection
{
    public static class ConfigureBindingsApplicationService
    {
        public static void RegisterBindings(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUrlApplicationService, UrlApplicationService>();

            #region Redis
            services.AddSingleton<IConnectionMultiplexer>(c =>
                ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection")));
            services.AddSingleton<ICacheService, RedisCacheService>();
            #endregion
        }
    }
}
