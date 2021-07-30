using Shortener.Domain.Modules;
using Shortener.Services.Notifications;
using System.Threading;
using System.Threading.Tasks;

namespace Shortener.Services.ApplicationService.BaseServices
{
    public class UrlService : BaseService
    {
        public UrlService(INotification notification) : base(notification)
        {

        }

        public async Task<string> RegisterUrl(Url url, CancellationToken cancellationToken = default)
        {
            return url.MainDestinationUrl;
        }
    }
}
