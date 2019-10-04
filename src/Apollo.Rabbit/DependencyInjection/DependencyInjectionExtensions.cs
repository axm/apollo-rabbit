using Apollo.Rabbit.Internal;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System;

namespace Apollo.Rabbit.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddRabbit(this IServiceCollection services, Action<RabbitConnectionOptions> action)
        {
            var options = new RabbitConnectionOptions();
            action.Invoke(options);

            var factory = new ConnectionFactory
            {
                HostName = options.HostName,
            };
            var manager = new RabbitConnectionManager(factory, options);

            services.AddSingleton(manager);

            return services;
        }
    }
}
