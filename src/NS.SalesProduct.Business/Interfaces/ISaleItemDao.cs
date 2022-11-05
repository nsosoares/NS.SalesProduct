using NS.SalesProduct.Business.Models;

namespace NS.SalesProduct.Business.Interfaces
{
    public interface ISaleItemDao : IDao<SaleItem>
    {
        Task<IEnumerable<SaleItem>> GetBySaleIdAsync(Guid saleId);
        Task DeleteBySaleIdAsync(Guid saleId);
        Task RegisterItemsAsync(IReadOnlyCollection<SaleItem> items);
    }
}
