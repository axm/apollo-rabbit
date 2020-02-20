using Apollo.Rabbit.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Apollo.Rabbit.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddRabbit(this IServiceCollection services, Action<RabbitOptions> action)
        {
            var options = new RabbitOptions();
            action.Invoke(options);

            services.AddSingleton<IRabbitConnectionFactory, RabbitConnectionFactory>();
            services.AddSingleton(options);

            //var manager = new RabbitConnectionManager(new RabbitConnectionFactory(), options);
            services.AddSingleton<IRabbitConnectionManager, RabbitConnectionManager>();
            services.AddSingleton<IRabbitChannelFactory, RabbitChannelFactory>();

            return services;
        }

        public static IServiceCollection AddRabbitPublisher<T>(this IServiceCollection services, Action<PublisherOptions<T>> setup)
        {
            var options = new PublisherOptions<T>();
            setup.Invoke(options);

            services.AddSingleton<IRabbitPublisher<T>, RabbitPublisher<T>>();
            services.AddSingleton(options);

            return services;
        }

        public static RabbitConsumerApp<TImplementation, T> AddRabbitConsumer<T, TImplementation>(this IServiceCollection services, Action<RabbitConsumerOptions> setup)
            where T : class
            where TImplementation: class, IRabbitConsumer<T>
        {
            var options = new RabbitConsumerOptions();
            setup.Invoke(options);

            services.AddSingleton(options);

            services.AddSingleton<IRabbitConsumer<T>, TImplementation>();
            services.AddSingleton<RabbitConsumerApp<TImplementation, T>>();

            var provider = services.BuildServiceProvider();
            return provider.GetService<RabbitConsumerApp<TImplementation, T>>();
        }
    }
}
