using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Shortener.Consumer.Consumers;
using Shortener.Services.ApplicationService;
using System;

namespace Shortener.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {


            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.ReceiveEndpoint("queue-url-shorten", e =>
                {
                    e.Consumer<UrlConsumer>();
                    e.PrefetchCount = 10;
                });
            });
            busControl.Start();

            Console.WriteLine("Waiting for messages...");

            while (true) ;
        }
    }
}
