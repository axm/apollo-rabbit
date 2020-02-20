using System.Collections.Generic;

namespace Apollo.Rabbit
{
    public sealed class BindingDeclaration
    {
        public string RoutingKey { get; set; }
        public IDictionary<string, object> Arguments { get; } = new Dictionary<string, object>();
        public QueueDeclaration Queue { get; set; }
        public ExchangeDeclaration Exchange { get; set; }
    }
}
