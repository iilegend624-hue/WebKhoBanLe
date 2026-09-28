using KhoBanLe.CoreBusiness;

namespace KhoBanLe.UseCases.PluginInterfaces.DataStore
{
    public interface IGoodsIssueRepository
    {
        Task<IEnumerable<GoodsIssue>> GetGoodsIssuesAsync(ReceiptStatus? status = null);
        Task<GoodsIssue?> GetGoodsIssueByIdAsync(int id);
        Task<int> CreateGoodsIssueAsync(GoodsIssue issue);
        Task ApproveIssueTransactionAsync(int issueId, string approvedBy);
        Task CancelIssueAsync(int issueId);
    }
}
