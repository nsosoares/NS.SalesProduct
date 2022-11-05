using FluentValidation.Results;
using NS.SalesProduct.Business.Interfaces;
using NS.SalesProduct.Business.Models;
using NS.SalesProduct.Business.Validations;
using NS.SalesProduct.Helpers.Messages;

namespace NS.SalesProduct.Business.Services
{
    public abstract class Service<TEntity, TValidation, TDao> 
        : IDisposable, IService<TEntity, TValidation>
        where TEntity : Entity
        where TValidation : Validation<TEntity>
        where TDao : IDao<TEntity>
    {
        protected readonly TDao _dao;
        protected readonly INotificationHandler _notifications;

        protected Service(TDao dao, INotificationHandler notifications)
        {
            _dao = dao;
            _notifications = notifications;
        }

        public virtual async Task RegisterAsync(TEntity entity, TValidation validation)
        {
            var isValid = await IsValidInDomainAsync(entity, validation);
            if (!isValid) return;
            entity.Generate();
            await _dao.RegisterAsync(entity);
        }

        public virtual async Task UpdateAsync(TEntity entity, TValidation validation, Guid id)
        {
            var isValid = await IsValidInDomainAsync(entity, validation);
            if (!isValid) return;

            var existingEntity = await _dao.GetByIdAsync(id) != null;
            if (!existingEntity)
            {
                Notify(TriggerMessage.TriggerMessageNotExisting(id.ToString()));
                return;
            }

            await _dao.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await _dao.GetByIdAsync(id);
            var existingEntity = entity != null;
            if (!existingEntity)
            {
                Notify(TriggerMessage.TriggerMessageNotExisting(id.ToString()));
                return;
            }

            await _dao.DeleteAsync(entity);
        }

        protected async Task<bool> IsValidInDomainAsync(TEntity entity, TValidation validation)
        {
            var isValid = await validation.IsValidAsync(entity);
            if (isValid) return true;

            Notify(validation.ValidationResult);

            return false;
        }

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var validation in validationResult.Errors)
                Notify(validation.ErrorMessage);
        }

        protected void Notify(string message)
           => _notifications.Handle(message);

        public void Dispose()
        {
            _dao.Dispose();
            _notifications.Dispose();
        }
    }
}
