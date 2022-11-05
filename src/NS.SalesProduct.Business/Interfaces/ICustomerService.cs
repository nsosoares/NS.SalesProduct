using NS.SalesProduct.Business.Models;
using NS.SalesProduct.Business.Validations;

namespace NS.SalesProduct.Business.Interfaces
{
    public interface ICustomerService : IService<Customer, CustomerValidation>
    {
    }
}
