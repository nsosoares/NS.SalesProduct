using NS.SalesProduct.Business.DTOs;

namespace NS.SalesProduct.Services.API.ViewModels
{
    public class ProductViewModel : Dto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal CostPrice { get; set; }
    }
}
