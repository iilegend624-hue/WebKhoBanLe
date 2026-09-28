using KhoBanLe.CoreBusiness;

namespace KhoBanLe.UseCases.PluginInterfaces.DataStore
{
    public interface IGoodsReceiptRepository
    {
        Task<IEnumerable<GoodsReceipt>> GetGoodsReceiptsAsync(ReceiptStatus? status = null);
        Task<GoodsReceipt?> GetGoodsReceiptByIdAsync(int id);
        Task<int> CreateGoodsReceiptAsync(GoodsReceipt receipt);
        Task ApproveReceiptTransactionAsync(int receiptId, string approvedBy);
        Task CancelReceiptAsync(int receiptId);
    }
}
