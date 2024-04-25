using ProcessamentoArquivos.Domain.Entities;

namespace ProcessamentoArquivos.Infraestructure.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(Guid id);
        Task AddAsync(TEntity entity);
        Task DeleteAsync (Guid id);
        Task UpdateAsync(TEntity entity);
    }
}
