using NS.SalesProduct.Business.Models;
using NS.SalesProduct.Business.Validations;

namespace NS.SalesProduct.Business.Interfaces
{
    public interface IService<TEntity, TValidation> : IDisposable
        where TEntity : Entity 
        where TValidation : Validation<TEntity>
    {
        Task RegisterAsync(TEntity entity, TValidation validation);
        Task UpdateAsync(TEntity entity, TValidation validation, Guid id);
        Task DeleteAsync(Guid id);
    }
}
