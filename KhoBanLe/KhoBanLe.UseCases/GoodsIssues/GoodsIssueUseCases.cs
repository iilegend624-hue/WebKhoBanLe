using KhoBanLe.CoreBusiness;
using KhoBanLe.UseCases.PluginInterfaces.DataStore;

namespace KhoBanLe.UseCases.GoodsIssues
{
    public interface ICreateGoodsIssueUseCase
    {
        Task<int> ExecuteAsync(GoodsIssue issue);
    }

    public class CreateGoodsIssueUseCase : ICreateGoodsIssueUseCase
    {
        private readonly IGoodsIssueRepository _issueRepository;

        public CreateGoodsIssueUseCase(IGoodsIssueRepository issueRepository)
        {
            _issueRepository = issueRepository;
        }

        public async Task<int> ExecuteAsync(GoodsIssue issue)
        {
            issue.Status = ReceiptStatus.Draft;
            return await _issueRepository.CreateGoodsIssueAsync(issue);
        }
    }

    public interface IViewGoodsIssueUseCase
    {
        Task<IEnumerable<GoodsIssue>> ExecuteListAsync(ReceiptStatus? status = null);
        Task<GoodsIssue?> ExecuteByIdAsync(int id);
    }

    public class ViewGoodsIssueUseCase : IViewGoodsIssueUseCase
    {
        private readonly IGoodsIssueRepository _issueRepository;

        public ViewGoodsIssueUseCase(IGoodsIssueRepository issueRepository)
        {
            _issueRepository = issueRepository;
        }

        public async Task<IEnumerable<GoodsIssue>> ExecuteListAsync(ReceiptStatus? status = null)
        {
            return await _issueRepository.GetGoodsIssuesAsync(status);
        }

        public async Task<GoodsIssue?> ExecuteByIdAsync(int id)
        {
            return await _issueRepository.GetGoodsIssueByIdAsync(id);
        }
    }

    public interface ICancelGoodsIssueUseCase
    {
        Task ExecuteAsync(int issueId);
    }

    public class CancelGoodsIssueUseCase : ICancelGoodsIssueUseCase
    {
        private readonly IGoodsIssueRepository _issueRepository;

        public CancelGoodsIssueUseCase(IGoodsIssueRepository issueRepository)
        {
            _issueRepository = issueRepository;
        }

        public async Task ExecuteAsync(int issueId)
        {
            await _issueRepository.CancelIssueAsync(issueId);
        }
    }
}
