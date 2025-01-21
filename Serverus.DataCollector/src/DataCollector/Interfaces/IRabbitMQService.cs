namespace Serverus.DataCollector.src.DataCollector.Interfaces
{
    public interface IRabbitMQService
    {
        Task PublishAsync<T>(string queueName, T message);
    }
}