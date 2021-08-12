using Microsoft.Extensions.Logging;
using Shortener.Common.DTO;
using Shortener.Domain.Modules;
using Shortener.Infrastructure.Persistence.Repository;
using Shortener.Services.ApplicationService.BaseServices;
using Shortener.Services.Cache;
using Shortener.Services.Notifications;
using System.Linq;
using System.Threading.Tasks;

namespace Shortener.Services.ApplicationService
{
    public class UpdateCounterJobApplicationService : BaseService, IUpdateCounterJobApplicationService
    {
        private readonly ILogger<UpdateCounterJobApplicationService> _logger;
        private readonly UrlRepository _urlRepository;
        private readonly ICacheService _cacheService;

        public UpdateCounterJobApplicationService(ILogger<UpdateCounterJobApplicationService> logger,
            UrlRepository urlRepository,
            ICacheService cacheService,
            INotification notification) : base(notification)
        {
            _logger = logger;
            _urlRepository = urlRepository;
            _cacheService = cacheService;
        }

        public async Task UpdateDailyCounter()
        {
            _logger.LogInformation("Atualizando Redis: URLs diarias.");

            var urls = await _urlRepository.GetUrlDayGreaterThanZero();
            var urlsDay = urls.OrderBy(x => x.DayCounter).Take(3).ToList();

            foreach (var url in urls)
            {
                url.UpdateWeeklyTotalCounter();
                url.ResetDayCounter();
                await _urlRepository.UpdateAsync(url.Id, url);
                await _cacheService.DeleteCacheValue($"key-{url.KeyUrl}");
            }

            foreach (var url in urlsDay)
            {
                var urlInfo = new UrlUpdateInfoDto
                {
                    Id = url.Id,
                    KeyUrl = url.KeyUrl,
                    MainDestinationUrl = url.MainDestinationUrl
                };

                await _cacheService.SetCacheValue<UrlUpdateInfoDto>($"key-{url.KeyUrl}", urlInfo);
            }

            return;
        }
    }
}
