using KhoBanLe.CoreBusiness;

namespace KhoBanLe.UseCases.PluginInterfaces.DataStore
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync(string? filter = null, string? category = null);
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product?> GetProductBySkuAsync(string sku);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task UpdateStockAsync(int productId, int quantityChange);
    }
}
