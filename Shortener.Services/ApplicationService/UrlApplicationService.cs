using FluentValidation;
using MassTransit;
using Shortener.Common.DTO;
using Shortener.Common.Events;
using Shortener.Common.Events.IEvents;
using Shortener.Domain.Modules;
using Shortener.Infrastructure.Persistence.Repository;
using Shortener.Services.ApplicationService.BaseServices;
using Shortener.Services.Cache;
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
        private readonly ICacheService _cacheService;

        public UrlApplicationService(UrlRepository urlRepository, 
            ICacheService cacheService,
            IPublishEndpoint endpoint, 
            INotification notification) : base(notification)
        {
            _endpoint = endpoint;
            _urlRepository = urlRepository;
            _cacheService = cacheService;
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

        public async Task<string> GetUrlByKey(string keyUrl)
        {
            if (!ExecuteValidations(keyUrl)) return string.Empty;

            // Verify if exists in Redis
            var haveUrlInRedis = await _cacheService.GetCacheValue<string>($"key-{keyUrl}");
            if (!string.IsNullOrEmpty(haveUrlInRedis)) return haveUrlInRedis;

            var result = await _urlRepository.GetUrlByKey(keyUrl);

            // TODO: Publish a message for change informations at url
            return string.IsNullOrEmpty(result.MainDestinationUrl) ? string.Empty : result.MainDestinationUrl;
        }


        private bool ExecuteValidations(string text)
        {
            var validacao = new KeyUrlValidation();
            var validator = validacao.Validate(text);

            if (validator.IsValid) return true;

            NotifyError(validator);

            return false;
        }
    }
}
