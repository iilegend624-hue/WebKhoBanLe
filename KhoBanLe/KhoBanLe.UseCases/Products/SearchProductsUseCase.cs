using KhoBanLe.CoreBusiness;
using KhoBanLe.UseCases.PluginInterfaces.DataStore;

namespace KhoBanLe.UseCases.Products
{
    public class SearchProductsUseCase : ISearchProductsUseCase
    {
        private readonly IProductRepository _productRepository;

        public SearchProductsUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> ExecuteAsync(string? filter = null, string? category = null)
        {
            return await _productRepository.GetProductsAsync(filter, category);
        }
    }
}
