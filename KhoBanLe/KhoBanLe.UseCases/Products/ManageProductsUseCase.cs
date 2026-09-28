using KhoBanLe.CoreBusiness;
using KhoBanLe.UseCases.PluginInterfaces.DataStore;

namespace KhoBanLe.UseCases.Products
{
    public interface IAddProductUseCase
    {
        Task ExecuteAsync(Product product);
    }

    public class AddProductUseCase : IAddProductUseCase
    {
        private readonly IProductRepository _productRepository;

        public AddProductUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task ExecuteAsync(Product product)
        {
            await _productRepository.AddProductAsync(product);
        }
    }

    public interface IEditProductUseCase
    {
        Task ExecuteAsync(Product product);
    }

    public class EditProductUseCase : IEditProductUseCase
    {
        private readonly IProductRepository _productRepository;

        public EditProductUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task ExecuteAsync(Product product)
        {
            await _productRepository.UpdateProductAsync(product);
        }
    }

    public interface IDeleteProductUseCase
    {
        Task ExecuteAsync(int id);
    }

    public class DeleteProductUseCase : IDeleteProductUseCase
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task ExecuteAsync(int id)
        {
            await _productRepository.DeleteProductAsync(id);
        }
    }
}
