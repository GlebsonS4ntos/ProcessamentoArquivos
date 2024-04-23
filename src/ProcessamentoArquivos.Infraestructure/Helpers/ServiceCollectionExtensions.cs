using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProcessamentoArquivos.Infraestructure.Data;
using ProcessamentoArquivos.Infraestructure.Interfaces;
using ProcessamentoArquivos.Infraestructure.Repositories;

namespace ProcessamentoArquivos.Infraestructure.Helpers
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfraestructure(this IServiceCollection service, IConfiguration configuration)
        {
            AddContext(service, configuration);
            AddRepositories(service);
        }

        private static void AddContext(IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<ProcessamentoArquivosContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("Arquivos"));
            });
        } 

        private static void AddRepositories(IServiceCollection service)
        {
            service.AddScoped<IRepositoryCliente, RepositoryCliente>();
        }
    }
}
