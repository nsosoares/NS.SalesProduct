using NS.SalesProduct.Business.Models;

namespace NS.SalesProduct.Business.Interfaces
{
    public interface ICustomerDao : IDao<Customer>
    {
        Task<Customer> GetByCpfAsync(string cpf);
    }
}
