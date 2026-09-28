using KhoBanLe.CoreBusiness;
using KhoBanLe.UseCases.PluginInterfaces.DataStore;

namespace KhoBanLe.UseCases.Products
{
    public interface IViewProductUseCase
    {
        Task<Product?> ExecuteAsync(int id);
        Task<Product?> ExecuteBySkuAsync(string sku);
    }

    public class ViewProductUseCase : IViewProductUseCase
    {
        private readonly IProductRepository _productRepository;

        public ViewProductUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product?> ExecuteAsync(int id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }

        public async Task<Product?> ExecuteBySkuAsync(string sku)
        {
            return await _productRepository.GetProductBySkuAsync(sku);
        }
    }
}
