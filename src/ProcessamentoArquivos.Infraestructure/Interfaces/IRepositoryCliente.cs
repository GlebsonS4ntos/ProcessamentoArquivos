using ProcessamentoArquivos.Domain.Entities;

namespace ProcessamentoArquivos.Infraestructure.Interfaces
{
    public interface IRepositoryCliente : IRepository<Cliente>
    {
        Task BulkInsertAsync(IEnumerable<Cliente> clienteList);
    }
}
