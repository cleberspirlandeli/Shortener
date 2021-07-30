using MassTransit;
using Shortener.Common.Events.IEvents;
using Shortener.Services.ApplicationService;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Shortener.Consumer.API.Consumers
{
    public class UrlConsumer : IConsumer<IUrlEvent>
    {

        private readonly IUrlApplicationService _urlApplicationService;
        public UrlConsumer(IUrlApplicationService urlApplicationService) => _urlApplicationService = urlApplicationService;

        public Task Consume(ConsumeContext<IUrlEvent> context)
        {
            _urlApplicationService.RegisterUrl(context.Message.Url);

            var idMessage = context.Message.IdMessage;
            var keyUrl = context.Message.Url.KeyUrl;
            Console.WriteLine($"Nova mensagem recebida: [{idMessage}] - {keyUrl}");
            return Task.CompletedTask;
        }
    }
}
