using ProcessamentoArquivos.Infraestructure.Helpers;
using ProcessamentoArquivos.Service.Helpers;
using Serilog;

namespace ProcessamentoArquivos.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Configuração do Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("../../logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var builder = Host.CreateApplicationBuilder(args);

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(Log.Logger, true); //adicionando o serilog como unico provedor de log

            builder.Services.AddHostedService<Worker>();

            builder.Services.AddInfraestructure(builder.Configuration);
            builder.Services.AddService();

            var host = builder.Build();

            try
            {
                Log.Information("Starting host...");
                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly!");
            }
            finally
            {
                Log.Information("Server Shutting down...");
                Log.CloseAndFlush();
            }
        }
    }
}