using KhoBanLe.CoreBusiness;

namespace KhoBanLe.UseCases.PluginInterfaces.DataStore
{
    public interface IInventoryReportRepository
    {
        Task<IEnumerable<InventoryReportItem>> GetInventoryReportAsync(DateTime? fromDate = null, DateTime? toDate = null);
    }
}
