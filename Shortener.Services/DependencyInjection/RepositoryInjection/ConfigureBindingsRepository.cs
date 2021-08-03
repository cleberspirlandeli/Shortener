using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shortener.Infrastructure.Persistence.Repository;

namespace Shortener.Services.DependencyInjection.RepositoryInjection
{
    public abstract class ConfigureBindingsRepository
    {
        public static void RegisterBindings(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<UrlRepository>();
        }
    }
}
