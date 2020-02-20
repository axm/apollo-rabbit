using System;

namespace Apollo.Rabbit
{
    public interface IMessage
    {
        Guid CorrelationId { get; set; }
        Guid MessageId { get; set; }
        DateTime TimestampCreated { get; set; }
    }
}
