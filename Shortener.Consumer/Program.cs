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

            Console.WriteLine("Shortener.Consumer: Waiting for messages...");

            while (true) ;
        }
    }
}
