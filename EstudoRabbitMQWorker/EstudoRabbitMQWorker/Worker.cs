using EstudoRabbitMQWorker.RequestModel;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace EstudoRabbitMQWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly int _intervaloMensagemWorkerAtivo;
        private readonly ParametrosRequest _parametrosRequest;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, ParametrosRequest parametrosRequest)
        {
            logger.LogInformation($"Queue = {parametrosRequest.Key}");
            _logger = logger;

            _intervaloMensagemWorkerAtivo = Convert.ToInt32(configuration["IntervaloMensagemWorkerAtivo"]);

            _parametrosRequest = parametrosRequest;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Aguardando mensagens...");

            var factory = new ConnectionFactory()
            {
                HostName = _parametrosRequest.Connection 
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(
                    queue: _parametrosRequest.Key,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += Consumer_Received;
            channel.BasicConsume(queue: _parametrosRequest.Key,
                autoAck: true,
                consumer);

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Worker ativo em: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                await Task.Delay(_intervaloMensagemWorkerAtivo, stoppingToken);
            }
        }

        private void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            _logger.LogInformation($"[Nova Mensagem | {DateTime.Now:yyyy-MM-dd HH:mm:ss}] " +
                Encoding.UTF8.GetString(e.Body.ToArray()));
        }
    }
}