using RabbitMQ.Client;

namespace Apollo.Rabbit
{
    public interface IRabbitConnectionFactory
    {
        IConnectionFactory GetConnectionFactory(string hostname, string virtualHost, string username, string password);
    }
}
