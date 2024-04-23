using Microsoft.Extensions.DependencyInjection;
using ProcessamentoArquivos.Service.Config;
using ProcessamentoArquivos.Service.Interfaces;
using ProcessamentoArquivos.Service.Services;

namespace ProcessamentoArquivos.Service.Helpers
{
    public static class ServiceCollectionExtensions
    {
        public static void AddService(this IServiceCollection service)
        {
            AutoMapper(service);
            Services(service);
        }

        private static void AutoMapper(IServiceCollection service)
        {
            service.AddScoped(provedor => new AutoMapper.MapperConfiguration(conf =>
            {
                conf.AddProfile(new AutoMapperConfig());
            }).CreateMapper());
        }

        private static void Services(IServiceCollection service)
        {
            service.AddScoped<IFileHandlerService, FileHandlerService>();
        }
    }
}