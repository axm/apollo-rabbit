using System.Collections.Generic;

namespace Apollo.Rabbit
{
    public class QueueDeclaration
    {
        public string Queue { get; set; }
        public bool Durable { get; set; }
        public bool Exclusive { get; set; }
        public bool AutoDelete { get; set; }
        public IDictionary<string, object> Arguments { get; set; } = new Dictionary<string, object>();
    }
}
