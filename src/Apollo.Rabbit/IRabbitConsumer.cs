using RabbitMQ.Client.Events;
using System.Threading.Tasks;

namespace Apollo.Rabbit
{
    public interface IRabbitConsumer<T>
    {
        Task ConsumeAsyc(T payload, BasicDeliverEventArgs ea);
    }
}
