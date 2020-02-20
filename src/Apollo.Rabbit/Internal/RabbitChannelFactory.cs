using RabbitMQ.Client;
using System;

namespace Apollo.Rabbit.Internal
{
    internal sealed class RabbitChannelFactory : IRabbitChannelFactory
    {
        public IModel CreateChannel(IConnection connection)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            return connection.CreateModel();
        }
    }
}
