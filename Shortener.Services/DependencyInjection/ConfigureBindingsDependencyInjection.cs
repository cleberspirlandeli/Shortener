using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shortener.Services.DependencyInjection.ApplicationServiceInjection;
using Shortener.Services.Notifications;

namespace Shortener.Services.DependencyInjection
{
    public class ConfigureBindingsDependencyInjection
    {
        public static void RegisterBindings(IServiceCollection services, IConfiguration configuration)
        {
            #region Others
            services.AddScoped<INotification, Notification>();
            #endregion

            #region Application Services
            ConfigureBindingsApplicationService.RegisterBindings(services, configuration);
            #endregion

            #region Repositories
            #endregion
        }
    }
}
