using MassTransit;
using Shortener.Common.DTO;
using Shortener.Common.Events;
using Shortener.Common.Events.IEvents;
using Shortener.Domain.Modules;
using Shortener.Infrastructure.Persistence.Repository;
using Shortener.Services.ApplicationService.BaseServices;
using Shortener.Services.Notifications;
using Shortener.Services.Validations;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Shortener.Services.ApplicationService
{
    public class UrlApplicationService : BaseService, IUrlApplicationService
    {
        private readonly IPublishEndpoint _endpoint;
        private readonly UrlRepository _urlRepository;
        public UrlApplicationService(UrlRepository urlRepository, 
            IPublishEndpoint endpoint, 
            INotification notification) : base(notification)
        {
            _endpoint = endpoint;
            _urlRepository = urlRepository;
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

        public void RegisterUrl(Url url, CancellationToken cancellationToken = default)
        {
            _urlRepository.Create(url);
        }

        public async Task<List<Url>> GetUrl()
        {
            var result = await _urlRepository.GetAll();
            return result;
        }

        public async Task<Url> GetUrlByKey(string id)
        {
            // TODO: Verify if exists in to redis

            var result = await _urlRepository.GetUrlByKey(id);

            // TODO: Publish a message for change informations at url
            return result;
        }

    }
}
