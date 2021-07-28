using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shortener.Services.ApplicationService;

namespace Shortener.Services.DependencyInjection.ApplicationServiceInjection
{
    public static class ConfigureBindingsApplicationService
    {
        public static void RegisterBindings(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUrlApplicationService, UrlApplicationService>();
        }
    }
}
