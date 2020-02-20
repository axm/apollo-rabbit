namespace Apollo.Rabbit
{
    public sealed class RabbitConnection
    {
        public string HostName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string VirtualHost { get; set; }
    }
}
