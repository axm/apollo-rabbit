using RabbitMQ.Client;

namespace Apollo.Rabbit.Internal
{
    internal sealed class RabbitConnectionManager : IRabbitConnectionManager
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly RabbitConnectionOptions _options;

        public RabbitConnectionManager(IConnectionFactory connectionFactory, RabbitConnectionOptions options)
        {
            _connectionFactory = connectionFactory;
            _options = options;
        }
        
        public IModel CreateChannel()
        {
            var connection = _connectionFactory.CreateConnection();
            var channel = connection.CreateModel();
            DeclareQueues(channel);

            return channel;
        }

        private void DeclareBindings(IModel channel)
        {
            foreach (var binding in _options.Bindings)
            {
                channel.ExchangeDeclare(binding.Exchange.Exchange,
                    binding.Exchange.Type,
                    binding.Exchange.Durable,
                    binding.Exchange.AutoDelete,
                    binding.Exchange.Arguments);
                channel.QueueDeclare(binding.Queue.Queue,
                    binding.Queue.Durable,
                    binding.Queue.Exclusive,
                    binding.Queue.AutoDelete,
                    binding.Queue.Arguments);
                channel.QueueBind(binding.Queue.Queue,
                    binding.Exchange.Exchange,
                    binding.RoutingKey,
                    binding.Arguments);
            }
        }

    }
}
