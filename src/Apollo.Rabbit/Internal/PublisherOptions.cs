namespace Apollo.Rabbit.Internal
{
    public sealed class PublisherOptions<T>
    {
        public string Exchange { get; set; }
        public string RoutingKey { get; set; }
    }
}
