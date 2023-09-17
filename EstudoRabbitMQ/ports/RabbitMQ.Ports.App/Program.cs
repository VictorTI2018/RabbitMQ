// See https://aka.ms/new-console-template for more information

using EstudoRabbitMQ.Adapters.RabbitMQService;
using EstudoRabbitMQ.Core.Shared.Utils.Logger;
using RabbitMQ.Ports.App.RequestModel;

static void Execute()
{
    var logger = ConsoleInformation.Execute();

    logger.Information("Testando o envio de mensagens para uma fila do RabbitMQ!!!");


    Console.Write("Conexão: ");
    var connection = Console.ReadLine();

    if (string.IsNullOrEmpty(connection)) throw new ArgumentException("Por favor, informe uma conexão.");

    Console.WriteLine(" ");

    Console.Write("Nome da Fila: ");
    var key = Console.ReadLine();

    if (string.IsNullOrEmpty(key)) throw new ArgumentException("Por favor, informe um nome para fila");

    Console.WriteLine(" ");

    bool executando = true;
    while(executando)
    {
        Console.Write("Mensagem: ");
        var message = Console.ReadLine();

        if (string.IsNullOrEmpty(message)) throw new ArgumentException("Por favor, informe uma mensagem");

        Console.WriteLine(" ");
        logger.Information($"Queue = {key}");

        QueueRequest queueRequest = new(key, connection, message);
        QueueService.Execute(queueRequest.Message, queueRequest.Connection, queueRequest.Key);

        logger.Information($"[Mensagem enviado] {message}");

        Console.WriteLine(" ");

        Console.Write("Deseja enviar mais mensagem? (S/N) ");
        var decisao = Console.ReadLine();

        
        if (string.IsNullOrEmpty(decisao))
        {
            logger.Information($"[Encerrando programa]");
            executando = false;
        } else if (decisao.ToLower() == "n")
        {
            logger.Information($"[Encerrando programa]");
            executando = false;
        }
    }
    

    Console.ReadKey();
}

try
{
    Execute();
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}