using System.Text.Json;
using System.Text;
using RabbitMQ.Client;
using Microsoft.Extensions.Configuration;
using Serverus.DataCollector.src.DataCollector.Interfaces;

namespace Serverus.DataCollector.src.DataCollector.Queue
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<RabbitMQService> _logger;

        public RabbitMQService(IConfiguration configuration, ILogger<RabbitMQService> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task PublishAsync<T>(string queueName, T message)
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = _configuration["RabbitMQ:Host"] ?? throw new ArgumentNullException("RabbitMQ:Host"),
                    UserName = _configuration["RabbitMQ:Username"] ?? throw new ArgumentNullException("RabbitMQ:Username"),
                    Password = _configuration["RabbitMQ:Password"] ?? throw new ArgumentNullException("RabbitMQ:Password")
                };

                using var connection = await factory.CreateConnectionAsync();
                using var channel = await connection.CreateChannelAsync();

                await channel.QueueDeclareAsync(
                    queue: queueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var json = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(json);

                await channel.BasicPublishAsync(
                    exchange: "",
                    routingKey: queueName,
                    mandatory: false,
                    body: body,
                    cancellationToken: default);

                _logger.LogInformation($"Message published to queue {queueName}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error publishing message to RabbitMQ");
                throw;
            }
        }
    }
}
