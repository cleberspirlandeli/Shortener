using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shortener.Domain;
using Shortener.Services.DependencyInjection.ApplicationServiceInjection;
using Shortener.Services.DependencyInjection.RepositoryInjection;
using Shortener.Services.Notifications;

namespace Shortener.Services.DependencyInjection
{
    public class ConfigureBindingsDependencyInjection
    {
        public static void RegisterBindings(IServiceCollection services, IConfiguration configuration)
        {
            // MongoDB Config
            //services.AddScoped<IUrlApplicationService, UrlApplicationService>();
            services.Configure<UrlShortenMongoDbSettings>(configuration.GetSection(nameof(UrlShortenMongoDbSettings)));
            services.AddSingleton<IUrlShortenMongoDbSettings>(sp =>
                sp.GetRequiredService<IOptions<UrlShortenMongoDbSettings>>().Value);

            #region Others
            services.AddScoped<INotification, Notification>();
            #endregion

            #region Application Services
            ConfigureBindingsApplicationService.RegisterBindings(services, configuration);
            #endregion

            #region Repositories
            ConfigureBindingsRepository.RegisterBindings(services, configuration);
            #endregion
        }
    }
}
