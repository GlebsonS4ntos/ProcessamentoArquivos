using Microsoft.EntityFrameworkCore;
using ProcessamentoArquivos.Infraestructure.Data;

namespace ProcessamentoArquivos.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            builder.Services.AddHostedService<Worker>();

            builder.Services.AddDbContext<ProcessamentoArquivosContext>(opt => 
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("Arquivos"));
            });

            var host = builder.Build();
            host.Run();
        }
    }
}