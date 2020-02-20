using RabbitMQ.Client;

namespace Apollo.Rabbit.Internal
{
    public sealed class RabbitConnectionFactory : IRabbitConnectionFactory
    {
        public IConnectionFactory GetConnectionFactory(string hostname, string virtualHost, string username, string password)
        {
            var factory = new ConnectionFactory
            {
                HostName = hostname,
                UserName = username,
                Password = password,
                VirtualHost = virtualHost,
            };

            return factory;
        }
    }
}
