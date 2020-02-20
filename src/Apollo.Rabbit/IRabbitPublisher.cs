namespace Apollo.Rabbit
{
    public interface IRabbitPublisher<T>
    {
        void Publish(T payload);
    }
}
