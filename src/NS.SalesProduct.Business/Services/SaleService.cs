using NS.SalesProduct.Business.Interfaces;
using NS.SalesProduct.Business.Models;
using NS.SalesProduct.Business.Validations;
using NS.SalesProduct.Helpers.Messages;

namespace NS.SalesProduct.Business.Services
{
    public class SaleService : Service<Sale, SaleValidation, ISaleDao>, ISaleService
    {
        private readonly ISaleItemDao _saleItemDao;
        public SaleService(ISaleDao dao, INotificationHandler notifications, ISaleItemDao saleItemDao)
            : base(dao, notifications)
        {
            _saleItemDao = saleItemDao;
        }

        public virtual async Task RegisterAsync(Sale entity, SaleValidation validation)
        {
            entity.Generate();
            GeneranteIds(entity.SaleItems);
            var isValid = await IsValidInDomainAsync(entity, validation);
            if (!isValid) return;
            await _dao.RegisterAsync(entity);
            await _saleItemDao.RegisterItemsAsync(entity.SaleItems);
        }

        public virtual async Task UpdateAsync(Sale entity, SaleValidation validation, Guid id)
        {
            GeneranteIds(entity.SaleItems);
            var isValid = await IsValidInDomainAsync(entity, validation);
            if (!isValid) return;

            var existingEntity = await _dao.GetByIdAsync(id) != null;
            if (!existingEntity)
            {
                Notify(TriggerMessage.TriggerMessageNotExisting(id.ToString()));
                return;
            }

            await _saleItemDao.DeleteBySaleIdAsync(id);
            await _saleItemDao.RegisterItemsAsync(entity.SaleItems);
            await _dao.UpdateAsync(entity);
        }

        private void GeneranteIds(IReadOnlyCollection<SaleItem> saleItems)
        {
            foreach (var item in saleItems)
                item.Generate();
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

            await _saleItemDao.DeleteBySaleIdAsync(entity.Id);
            await _dao.DeleteAsync(entity);
        }
    }
}
