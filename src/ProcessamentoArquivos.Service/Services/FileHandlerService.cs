using ProcessamentoArquivos.Infraestructure.Interfaces;
using ProcessamentoArquivos.Service.Interfaces;

namespace ProcessamentoArquivos.Service.Services
{
    public class FileHandlerService : IFileHandlerService
    {
        private readonly IRepositoryCliente _repository;

        public FileHandlerService(IRepositoryCliente repository)
        {
            _repository = repository;
        }

        public Task ReadFilesAsync()
        {
            throw new NotImplementedException();
        }
    }
}