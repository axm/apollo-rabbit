using Microsoft.Extensions.DependencyInjection;
using Apollo.Rabbit.DependencyInjection;
using Apollo.Rabbit.Extensions;
using System;
using Apollo.Rabbit;
using Apollo.Rabbit.Internal;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace RabbitConsumer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var options = new RabbitOptions
            {
                Connection = new RabbitConnection
                {
                    HostName = "localhost",
                    Username = "guest",
                    Password = "guest",
                    VirtualHost = "/",
                },
                Bindings = GetBindings()
            };

            var manager = new RabbitConnectionManager(new RabbitConnectionFactory(), options);

            var channel = manager.GetChannel();
            var cts = new CancellationTokenSource();
            channel.Publish("SampleEvents", "SampleEvents", new SampleEvent { Id = -101, Greeting = "Mayfair" });

            channel.Consume<SampleEvent>(options.Bindings[0].Queue.Queue, true, async se =>
            {
                await Task.CompletedTask;

                cts.Cancel();
            });

            await Task.Delay(-1, cts.Token);
        }

        private static BindingDeclaration[] GetBindings()
        {
            return new[]
            {
                new BindingDeclaration
                {
                    Exchange = new ExchangeDeclaration
                    {
                        Exchange = "SampleEvents",
                        Type = "direct",
                        Durable = true,
                        AutoDelete = false,
                    },
                    Queue = new QueueDeclaration
                    {
                        Queue = "SampleEvents",
                        Durable = true,
                        AutoDelete = false,
                        Exclusive = false
                    },
                    RoutingKey = "SampleEvents",
                }
            };
        }

        private class SampleEvent
        {
            public int Id { get; set; }
            public string Greeting { get; set; }
        }
    }
}
