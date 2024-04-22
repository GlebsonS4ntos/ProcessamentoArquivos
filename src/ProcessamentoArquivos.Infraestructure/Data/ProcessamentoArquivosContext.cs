using Microsoft.EntityFrameworkCore;
using ProcessamentoArquivos.Domain.Entities;
using System.Reflection;

namespace ProcessamentoArquivos.Infraestructure.Data
{
    public class ProcessamentoArquivosContext : DbContext
    {
        public ProcessamentoArquivosContext(DbContextOptions<ProcessamentoArquivosContext> opt) : base(opt){ }

        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
