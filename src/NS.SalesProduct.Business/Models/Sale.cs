using NS.SalesProduct.Business.Enuns;

namespace NS.SalesProduct.Business.Models
{
    public class Sale : Entity
    {
        private List<SaleItem> _saleItems;

        public Sale(
          Guid id,
          Guid customerId,
          decimal totalPrice,
          int? discount,
          EPaymentMethod paymentMethod) : base(id)
        {
            CustomerId = customerId;
            TotalPrice = totalPrice;
            Discount = discount;
            PaymentMethod = paymentMethod;
            PricePaid = CalculatePricePaid();
            _saleItems = new List<SaleItem>();
        }

        public Guid CustomerId { get; private set; }
        public decimal TotalPrice { get; private set; }
        public int? Discount { get; private set; }
        public EPaymentMethod PaymentMethod { get; private set; }
        public decimal PricePaid { get; private set; }

        // NAVIGATION PROPERTIES
        public Customer Customer { get; private set; }
        public IReadOnlyCollection<SaleItem> SaleItems => _saleItems;

        public decimal CalculatePricePaid()
        {
            var mustDiscount = Discount.HasValue && Discount.Value > 0;
            if (!mustDiscount) return TotalPrice;
            var valueToDiscount = (TotalPrice * Discount.Value) / 100;
            return TotalPrice - valueToDiscount;
        }

        public void AddItems(List<SaleItem> items) 
            => _saleItems.AddRange(items);

        public void AddItem(SaleItem item)
           => _saleItems.Add(item);
    }
}
