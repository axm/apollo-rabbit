using System.Collections.Generic;

namespace Apollo.Rabbit
{
    public sealed class ExchangeDeclaration
    {
        public string Exchange { get; set; }
        public string Type { get; set; }
        public bool Durable { get; set; }
        public bool AutoDelete { get; set; }
        public IDictionary<string, object> Arguments { get; set; } = new Dictionary<string, object>();
    }
}
