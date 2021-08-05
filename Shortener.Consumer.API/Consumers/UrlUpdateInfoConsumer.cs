using MassTransit;
using Shortener.Common.Events.IEvents;
using Shortener.Services.ApplicationService;
using System.Threading.Tasks;

namespace Shortener.Consumer.API.Consumers
{
    public class UrlUpdateInfoConsumer : IConsumer<IUrlUpdateInfoEvent>
    {
        private readonly IUrlApplicationService _urlApplicationService;
        public UrlUpdateInfoConsumer(IUrlApplicationService urlApplicationService)
            => _urlApplicationService = urlApplicationService;

        public Task Consume(ConsumeContext<IUrlUpdateInfoEvent> context)
        {
            _urlApplicationService.UrlUpdateInfo(context.Message.Data);

            return Task.CompletedTask;
        }
    }
}
