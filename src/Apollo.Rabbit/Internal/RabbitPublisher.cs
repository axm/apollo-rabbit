using Apollo.Rabbit.Extensions;

namespace Apollo.Rabbit.Internal
{
    internal sealed class RabbitPublisher<T> : IRabbitPublisher<T>
    {
        private readonly IRabbitConnectionManager _connectionManager;
        private readonly PublisherOptions<T> _options;

        public RabbitPublisher(IRabbitConnectionManager connectionManager, PublisherOptions<T> options)
        {
            _connectionManager = connectionManager;
            _options = options;
        }

        public void Publish(T payload)
        {
            var channel = _connectionManager.GetChannel();
            channel.Publish(_options.Exchange, _options.RoutingKey, payload);
        }
    }
}
