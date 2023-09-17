using EstudoRabbitMQWorker;
using EstudoRabbitMQWorker.RequestModel;


static IHostBuilder CreateHostBuilder(string[] args)
{
    return Host.CreateDefaultBuilder(args)
     .ConfigureServices(services =>
     {
         services.AddSingleton(
             new ParametrosRequest
             {
                 Connection = args[0],
                 Key = args[1]
             }).AddHostedService<Worker>();
     });

    //await host.RunAsync();
}

static void Execute()
{
    Console.Write("Conexão: ");
    var connection = Console.ReadLine();

    Console.WriteLine(" ");

    Console.Write("Queue: ");
    var key = Console.ReadLine();

    string[] args = new string[] { connection, key };
    CreateHostBuilder(args).Build().Run();
}

Execute();

