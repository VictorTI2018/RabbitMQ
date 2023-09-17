using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace EstudoRabbitMQ.Adapters.RabbitMQService
{
    public class QueueService
    {
        /// <summary>
        /// Enviar mensagens para fila
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="connectionString"></param>
        /// <param name="key"></param>
        public static void Execute<T>(T message, string connectionString, string key)
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = connectionString
            };

            using var createConnection = connectionFactory.CreateConnection();
            using var channel = createConnection.CreateModel();

            channel.QueueDeclare(queue: key,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var json = JsonConvert.SerializeObject(message);

            channel.BasicPublish(exchange: "", routingKey: key, basicProperties: null, body: Encoding.UTF8.GetBytes(json));
        }
    }
}