using RabbitMQ.Client;

namespace Apollo.Rabbit
{
    public interface IRabbitConnectionManager
    {
        IModel GetChannel();
    }
}
