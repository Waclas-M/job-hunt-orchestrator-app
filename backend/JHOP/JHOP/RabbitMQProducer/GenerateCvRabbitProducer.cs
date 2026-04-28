using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
namespace JHOP.RabbitMQProducer
{
    public static class GenerateCvRabbitProducer
    {
        public static async Task SendMessage(object message)
        {
            var factory = new ConnectionFactory() { HostName = "rabbitmq" };
            using (var connection = await factory.CreateConnectionAsync())
            using (var channel = await connection.CreateChannelAsync())
            {
                await channel.QueueDeclareAsync(queue: "generate_cv_queue",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var json = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(json);

                await channel.BasicPublishAsync(
                    exchange: string.Empty,
                    routingKey: "generate_cv_queue",
                    mandatory: true,
                    basicProperties: new BasicProperties { Persistent = true },
                    body: body);
               
            }
        }

    }
}
