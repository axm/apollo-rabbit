using RabbitMQ.Client;

namespace Apollo.Rabbit
{
    public interface IRabbitChannelFactory
    {
        IModel CreateChannel(IConnection connection);
    }
}
