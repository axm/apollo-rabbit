using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Apollo.Rabbit.Extensions
{
    public static class ChannelExtensions
    {
        private static readonly byte NonPersistent = 1;
        private static readonly byte Persistent = 2;

        public static void Consume<T>(this IModel channel, string queue, Func<T, Task> callback)
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async(ch, ea) =>
            {
                try
                {
                    var msg = JsonSerializer.Deserialize<T>(ea.Body);

                    await callback.Invoke(msg);
                }
                catch (Exception e)
                {
                    return;
                }

                channel.BasicAck(ea.DeliveryTag, false);
            };

            channel.BasicConsume(consumer, queue, autoAck: false);
        }

        public static void Consume<T>(this IModel channel, string queue, Func<T, BasicDeliverEventArgs, Task> callback)
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (ch, ea) =>
            {
                try
                {
                    var msg = JsonSerializer.Deserialize<T>(ea.Body);

                    await callback.Invoke(msg, ea);
                }
                catch (Exception e)
                {
                    return;
                }

                channel.BasicAck(ea.DeliveryTag, false);
            };
            channel.BasicConsume(consumer, queue, autoAck: false);
        }

        public static void Publish<T>(this IModel model, string exchange, string routingKey, T payload)
        {
            var body = JsonSerializer.SerializeToUtf8Bytes<T>(payload);
            var basicProperties = model.CreateBasicProperties();
            basicProperties.DeliveryMode = Persistent;
            basicProperties.ContentType = "application/json";
            model.BasicPublish(exchange, routingKey, basicProperties, body);
        }
    }
}
