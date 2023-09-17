using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace EstudoRabbitMQ.Core.Shared.Utils.Logger
{
    public class ConsoleInformation
    {
        public static Serilog.Core.Logger Execute()
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Console(theme: AnsiConsoleTheme.Literate)
                .CreateLogger();

            return logger;
        }
    }
}
