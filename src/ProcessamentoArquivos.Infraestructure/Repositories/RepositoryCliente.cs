using EFCore.BulkExtensions;
using ProcessamentoArquivos.Domain.Entities;
using ProcessamentoArquivos.Infraestructure.Data;
using ProcessamentoArquivos.Infraestructure.Interfaces;

namespace ProcessamentoArquivos.Infraestructure.Repositories
{
    public class RepositoryCliente : Repository<Cliente>, IRepositoryCliente
    {
        private readonly ProcessamentoArquivosContext _context;  

        public RepositoryCliente(ProcessamentoArquivosContext context) : base(context) 
        { 
            _context = context;
        }

        public async Task BulkInsertAsync (IEnumerable<Cliente> clienteList)
        {
            await _context.BulkInsertAsync(clienteList);
        }
    }
}
