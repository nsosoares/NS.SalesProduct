using NS.SalesProduct.Business.DTOs;

namespace NS.SalesProduct.Services.API.ViewModels
{
    public class SaleItemViewModel : Dto
    {
        public Guid ProductId { get; set; }
        public Guid SaleId { get; set; }
        public int Amount { get; set; }
    }
}
