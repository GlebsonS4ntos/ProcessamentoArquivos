using ProcessamentoArquivos.Infraestructure.Helpers;
using ProcessamentoArquivos.Service.Helpers;

namespace ProcessamentoArquivos.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<Worker>();

            builder.Services.AddInfraestructure(builder.Configuration);
            builder.Services.AddService();

            var host = builder.Build();
            host.Run();
        }
    }
}