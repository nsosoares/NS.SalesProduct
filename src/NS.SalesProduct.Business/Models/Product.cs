namespace NS.SalesProduct.Business.Models
{
    public class Product : Entity
    {
        public Product(Guid id, string name, decimal price, decimal costPrice)
            : base(id)
        {
            Name = name;
            Price = price;
            CostPrice = costPrice;
        }

        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public decimal CostPrice { get; private set; }

        // NAVIGATION PROPERTIES
        public IReadOnlyCollection<SaleItem> SaleItems { get; private set; }

        public void ChangePrice(decimal newPrice)
            => Price = newPrice;
    }
}
