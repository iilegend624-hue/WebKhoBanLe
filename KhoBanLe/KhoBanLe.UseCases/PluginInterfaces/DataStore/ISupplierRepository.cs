using KhoBanLe.CoreBusiness;

namespace KhoBanLe.UseCases.PluginInterfaces.DataStore
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetSuppliersAsync();
        Task<Supplier?> GetSupplierByIdAsync(int id);
        Task AddSupplierAsync(Supplier supplier);
        Task UpdateSupplierAsync(Supplier supplier);
    }
}
