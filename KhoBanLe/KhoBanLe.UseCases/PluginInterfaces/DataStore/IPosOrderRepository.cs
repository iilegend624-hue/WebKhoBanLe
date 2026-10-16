using KhoBanLe.CoreBusiness;

namespace KhoBanLe.UseCases.PluginInterfaces.DataStore
{
    public interface IPosOrderRepository
    {
        Task<IEnumerable<PosOrder>> GetPosOrdersAsync(DateTime? fromDate = null, DateTime? toDate = null);
        Task<PosOrder?> GetPosOrderByIdAsync(int id);
        Task<PosOrder?> GetPosOrderByNumberAsync(string orderNumber);
        Task<int> CreatePosOrderAsync(PosOrder order);
        Task<bool> CancelPosOrderAsync(int id, string reason = "");
    }
}
