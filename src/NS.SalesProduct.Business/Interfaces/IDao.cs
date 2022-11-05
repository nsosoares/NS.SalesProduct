using NS.SalesProduct.Business.Models;

namespace NS.SalesProduct.Business.Interfaces
{
    public interface IDao<TEntity> : IDisposable where TEntity : Entity
    {
        Task<IReadOnlyCollection<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(Guid id);
        Task RegisterAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
