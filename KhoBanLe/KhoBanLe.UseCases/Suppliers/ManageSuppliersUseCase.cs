using KhoBanLe.CoreBusiness;
using KhoBanLe.UseCases.PluginInterfaces.DataStore;

namespace KhoBanLe.UseCases.Suppliers
{
    public interface IGetSuppliersUseCase
    {
        Task<IEnumerable<Supplier>> ExecuteAsync();
    }

    public class GetSuppliersUseCase : IGetSuppliersUseCase
    {
        private readonly ISupplierRepository _supplierRepository;

        public GetSuppliersUseCase(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<IEnumerable<Supplier>> ExecuteAsync()
        {
            return await _supplierRepository.GetSuppliersAsync();
        }
    }
}
