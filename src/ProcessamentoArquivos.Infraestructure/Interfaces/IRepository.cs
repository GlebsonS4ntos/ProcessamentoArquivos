using ProcessamentoArquivos.Domain.Entities;

namespace ProcessamentoArquivos.Infraestructure.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(Guid id);
        Task Add(TEntity entity);
        Task Delete (Guid id);
        Task Update(TEntity entity);
    }
}
