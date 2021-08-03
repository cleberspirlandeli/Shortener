using MassTransit;
using Shortener.Common.Events.IEvents;
using Shortener.Services.ApplicationService;
using System.Threading.Tasks;

namespace Shortener.Consumer.API.Consumers
{
    public class UrlConsumer : IConsumer<IUrlEvent>
    {

        private readonly IUrlApplicationService _urlApplicationService;
        public UrlConsumer(IUrlApplicationService urlApplicationService) 
            => _urlApplicationService = urlApplicationService;

        public Task Consume(ConsumeContext<IUrlEvent> context)
        {
            _urlApplicationService.RegisterUrl(context.Message.Url);

            return Task.CompletedTask;
        }
    }
}
