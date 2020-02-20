﻿using System;

namespace Apollo.Rabbit
{
    public sealed class RabbitOptions
    {
        public RabbitConnection Connection { get; set; }
        public BindingDeclaration[] Bindings { get; set; } = Array.Empty<BindingDeclaration>();

        public bool ReuseConnections { get; set; } = true;
        public bool ReuseChannels { get; set; } = true;
    }

    public static class RabbitOptionsExtensions
    {
        public static RabbitOptions RegisterPublisher<T>(this RabbitOptions options, string exchange, string queue)
        {
            throw new NotImplementedException();
        }
    }
}
