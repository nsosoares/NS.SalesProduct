namespace NS.SalesProduct.Business.Models
{
    public class SaleItem : Entity
    {
        public SaleItem(Guid id, Guid productId, Guid saleId, int amount)
            :base(id)
        {
            ProductId = productId;
            SaleId = saleId;
            Amount = amount;
        }

        public Guid ProductId { get; private set; }
        public Guid SaleId { get; private set; }
        public int Amount { get; private set; }

        // NAVIGATION PROPERTIES
        public Product Product { get; private set; }
        public Sale Sale { get; private set; }
    }
}
