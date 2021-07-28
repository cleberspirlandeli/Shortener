using Domain.Modules;
using MassTransit;
using Shortener.Common.DTO;
using Shortener.Services.ApplicationService.BaseServices;
using Shortener.Services.Notifications;
using Shortener.Services.Validations;
using System;
using System.Threading.Tasks;

namespace Shortener.Services.ApplicationService
{
    public class UrlApplicationService : BaseService, IUrlApplicationService
    {
        private readonly IPublishEndpoint _bus;
        public UrlApplicationService(INotification notification) : base(notification)
        {
        }

        public async Task<string> GenerateShorterUrl(UrlDto dto)
        {
            var url = new Url(dto.MainDestinationUrl);
            if (!ExecuteValidations(new UrlValidation(), url)) return string.Empty;

            _bus.Publish<Url>(url);

            return $"rzn.io/{url.KeyUrl}";
        }
    }
}
