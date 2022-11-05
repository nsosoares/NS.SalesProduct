using NS.SalesProduct.Business.Interfaces;
using NS.SalesProduct.Business.Models;
using NS.SalesProduct.Business.Validations;

namespace NS.SalesProduct.Business.Services
{
    public class ProductService : Service<Product, ProductValidation, IProductDao>, IProductService
    {
        public ProductService(IProductDao dao, INotificationHandler notifications) : base(dao, notifications)
        {
        }
    }
}
