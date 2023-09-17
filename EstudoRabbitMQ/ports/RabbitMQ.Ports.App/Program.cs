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
    List<TodoRequest> todosRequest = new();
    while(executando)
    {
        Console.Write("Titulo: ");
        var title = Console.ReadLine();

        if (string.IsNullOrEmpty(title)) throw new ArgumentException("Por favor, informe um titulo para tarefa");

        Console.WriteLine(" ");

        Console.Write("Descrição: ");
        var description = Console.ReadLine();

        if (string.IsNullOrEmpty(description)) throw new ArgumentException("Por favor, informe uma descrição para tarefa");

        TodoRequest dados = new(title, description);


        todosRequest.Add(dados);


        Console.WriteLine(" ");

        Console.Write("Deseja cadastrar mais mensagem tarefas? (S/N) ");
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

    Console.WriteLine(" ");
    logger.Information($"Queue = {key}");

    QueueRequest queueRequest = new(key, connection, todosRequest);
    QueueService.Execute(queueRequest.Todo, queueRequest.Connection, queueRequest.Key);

    logger.Information($"[Tarefa enviada]...");

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