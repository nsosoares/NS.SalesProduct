using NS.SalesProduct.Business.Interfaces;
using NS.SalesProduct.Business.Models;
using NS.SalesProduct.Business.Validations;
using NS.SalesProduct.Helpers.Messages;

namespace NS.SalesProduct.Business.Services
{
    public class CustomerService : Service<Customer, CustomerValidation, ICustomerDao>, ICustomerService
    {
        public CustomerService(ICustomerDao dao, INotificationHandler notifications) : base(dao, notifications)
        {
        }

        public override async Task RegisterAsync(Customer entity, CustomerValidation validation)
        {
            entity.Generate();
            var isValid = await IsValidInDomainAsync(entity, validation);
            if (!isValid) return;
            var existingCpf = await _dao.GetByCpfAsync(entity.Cpf) != null;
            if (existingCpf)
            {
                Notify(TriggerMessage.TriggerMessageExistingItem("CPF", entity.Cpf));
                return;
            }

            await _dao.RegisterAsync(entity);
        }

        public override async Task UpdateAsync(Customer entity, CustomerValidation validation, Guid id)
        {
            var isValid = await IsValidInDomainAsync(entity, validation);
            if (!isValid) return;

            var existingEntity = await _dao.GetByIdAsync(id) != null;
            if (!existingEntity)
            {
                Notify(TriggerMessage.TriggerMessageNotExisting(id.ToString()));
                return;
            }

            var entityExisting = await _dao.GetByCpfAsync(entity.Cpf);
            var existingCpf = entityExisting != null && entityExisting.Id != id;
            if (existingCpf)
            {
                Notify(TriggerMessage.TriggerMessageExistingItem("CPF", entity.Cpf));
                return;
            }

            await _dao.UpdateAsync(entity);
        }
    }
}
