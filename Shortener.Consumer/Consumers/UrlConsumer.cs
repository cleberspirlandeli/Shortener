using MassTransit;
using Shortener.Common.Events.IEvents;
using Shortener.Services.ApplicationService;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shortener.Services.ApplicationService.BaseServices;

namespace Shortener.Consumer.Consumers
{
    public class UrlConsumer : IConsumer<IUrlEvent>
    {

        //private ServiceProvider _serviceProvider;

        //public UrlConsumer(ServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public Task Consume(ConsumeContext<IUrlEvent> context)
        {

            var serviceProvider = new ServiceCollection()
                //.AddSingleton<IUrlApplicationService, UrlApplicationService>()
                .AddSingleton<UrlService>()
                .BuildServiceProvider();

            var _urlApplicationService = serviceProvider.GetService<UrlService>();


            var url = context.Message.Url;
            _urlApplicationService.RegisterUrl(url);
            //_urlApplicationService.RegisterUrl(url);


            var idMessage = context.Message.IdMessage;
            var keyUrl = context.Message.Url.KeyUrl;
            Console.WriteLine($"Nova mensagem recebida: [{idMessage}] - {keyUrl}");
            return Task.CompletedTask;
        }
    }
}
