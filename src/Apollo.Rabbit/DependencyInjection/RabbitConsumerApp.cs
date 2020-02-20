using Apollo.Rabbit.Extensions;
using System.Threading;

namespace Apollo.Rabbit.DependencyInjection
{
    public sealed class RabbitConsumerApp<T, U>
        where T: class, IRabbitConsumer<U>
    {
        private readonly IRabbitConnectionManager _manager;
        private readonly T _consumer;
        private readonly RabbitConsumerOptions _options;

        public RabbitConsumerApp(IRabbitConnectionManager manager, T consumer, RabbitConsumerOptions options)
        {
            _manager = manager;
            _consumer = consumer;
            _options = options;
        }

        public void Start(CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return;

            var channel = _manager.GetChannel();
            if (token.IsCancellationRequested)
                channel.Close();

            channel.Consume<U>(_options.Queue, async (se, ev) =>
            {
                if (token.IsCancellationRequested)
                {
                    channel.Close();
                    return;
                }

                await _consumer.ConsumeAsyc(se, ev);

                if (token.IsCancellationRequested)
                    channel.Close();
            });
        }
    }
}
