using RabbitMQ.Client.Events;
using System.Threading.Tasks;

namespace Apollo.Rabbit
{
    public interface IRabbitRawConsumer
    {
        Task ConsumeAsync(BasicDeliverEventArgs ea);
    }
}
