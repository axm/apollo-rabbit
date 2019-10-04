using System;

namespace Apollo.Rabbit
{
    public sealed class RabbitConnectionOptions
    {
        public string HostName { get; set; }
        public BindingDeclaration[] Bindings { get; set; } = Array.Empty<BindingDeclaration>();
    }
}
