using NS.SalesProduct.Business.Models;
using NS.SalesProduct.Business.Validations;

namespace NS.SalesProduct.Business.Interfaces
{
    public interface IProductService : IService<Product, ProductValidation>
    {
    }
}
