using MassTransit;
using Shortener.Common.DTO;
using Shortener.Common.Events;
using Shortener.Common.Events.IEvents;
using Shortener.Domain.Modules;
using Shortener.Services.ApplicationService.BaseServices;
using Shortener.Services.Notifications;
using Shortener.Services.Validations;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shortener.Services.ApplicationService
{
    public class UrlApplicationService : BaseService, IUrlApplicationService
    {
        private readonly IPublishEndpoint _endpoint;
        public UrlApplicationService(IPublishEndpoint endpoint, INotification notification) : base(notification)
        {
            _endpoint = endpoint;
        }

        public async Task<string> GenerateShorterUrl(UrlDto dto, CancellationToken cancellationToken = default)
        {
            var url = new Url(dto.MainDestinationUrl);
            if (!ExecuteValidations(new UrlValidation(), url)) return string.Empty;

            await _endpoint.Publish<IUrlEvent>(new UrlEvent 
            {
                IdMessage = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                Url = url
            }, cancellationToken);

            return $"rzn.cc/{url.KeyUrl}";
        }

        public async Task<string> RegisterUrl(Url url, CancellationToken cancellationToken = default)
        {
            return url.MainDestinationUrl;
        }

    }
}
