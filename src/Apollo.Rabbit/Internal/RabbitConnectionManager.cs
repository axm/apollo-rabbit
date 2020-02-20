using RabbitMQ.Client;

namespace Apollo.Rabbit.Internal
{
    public sealed class RabbitConnectionManager : IRabbitConnectionManager
    {
        private readonly IRabbitConnectionFactory _connectionFactory;
        private readonly IRabbitChannelFactory _rabbitChannelFactory;
        private readonly RabbitOptions _options;
        private IConnection Connection;
        private IModel Channel;

        public RabbitConnectionManager(IRabbitConnectionFactory connectionFactory, IRabbitChannelFactory rabbitChannelFactory, RabbitOptions options)
        {
            _connectionFactory = connectionFactory;
            _rabbitChannelFactory = rabbitChannelFactory;
            _options = options;
        }

        public IModel GetChannel()
        {
            if (Connection == null || !Connection.IsOpen)
            {
                Connection = CreateConnection();
            }

            if (Channel == null || Channel.IsClosed)
            {
                Channel = _rabbitChannelFactory.CreateChannel(Connection);
            }

            DeclareBindings(Channel);

            return Channel;
        }

        private IConnection CreateConnection()
            => _connectionFactory.GetConnectionFactory(_options.Connection.HostName, _options.Connection.VirtualHost, _options.Connection.Username, _options.Connection.Password).CreateConnection();

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
