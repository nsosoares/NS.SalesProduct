using NS.SalesProduct.Business.DTOs;
using NS.SalesProduct.Business.Enuns;

namespace NS.SalesProduct.Services.API.ViewModels
{
    public class SaleViewModel : Dto
    {
        public Guid CustomerId { get; set; }
        public decimal TotalPrice { get; set; }
        public int? Discount { get; set; }
        public EPaymentMethod PaymentMethod { get; set; }
        public List<SaleItemViewModel> SaleItems { get; set; }
    }
}
