using KhoBanLe.CoreBusiness;

namespace KhoBanLe.UseCases.Products
{
    public interface ISearchProductsUseCase
    {
        Task<IEnumerable<Product>> ExecuteAsync(string? filter = null, string? category = null);
    }
}
