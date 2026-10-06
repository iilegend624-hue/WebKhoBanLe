using KhoBanLe.CoreBusiness;
using KhoBanLe.UseCases.PluginInterfaces.DataStore;

namespace KhoBanLe.DataStore.HardCoded
{
    public class GoodsIssueRepository : IGoodsIssueRepository
    {
        private readonly List<GoodsIssue> _issues;
        private readonly IProductRepository _productRepository;

        public GoodsIssueRepository(IProductRepository productRepository)
        {
            _productRepository = productRepository;

            _issues = new List<GoodsIssue>
            {
                new GoodsIssue
                {
                    Id = 1,
                    IssueNumber = "PX-2026-001",
                    CustomerOrDepartment = "Cửa hàng bán lẻ Chi nhánh 1",
                    IssueDate = DateTime.Now.AddDays(-3),
                    CreatedBy = "Thủ kho Nguyễn Văn A",
                    ApprovedBy = "Quản lý Trần Thị B",
                    ApprovedDate = DateTime.Now.AddDays(-3).AddHours(2),
                    Status = ReceiptStatus.Approved,
                    Note = "Xuất hàng bày kệ chi nhánh 1",
                    Lines = new List<GoodsIssueLine>
                    {
                        new GoodsIssueLine
                        {
                            Id = 1,
                            GoodsIssueId = 1,
                            ProductId = 2, // Mì Hảo Hảo
                            Quantity = 10,
                            UnitPrice = 115000
                        },
                        new GoodsIssueLine
                        {
                            Id = 2,
                            GoodsIssueId = 1,
                            ProductId = 3, // Sữa Vinamilk
                            Quantity = 5,
                            UnitPrice = 34000
                        }
                    }
                },
                new GoodsIssue
                {
                    Id = 2,
                    IssueNumber = "PX-2026-002",
                    CustomerOrDepartment = "Bộ phận Hành chính - Văn phòng",
                    IssueDate = DateTime.Now.AddHours(-4),
                    CreatedBy = "Thủ kho Nguyễn Văn A",
                    ApprovedBy = null,
                    ApprovedDate = null,
                    Status = ReceiptStatus.Draft, // Chờ duyệt
                    Note = "DEMO LUẬT 1: Yêu cầu xuất 10 bút bi Thiên Long (Tồn kho chỉ có 5) -> Bấm Duyệt sẽ bị chặn!",
                    Lines = new List<GoodsIssueLine>
                    {
                        new GoodsIssueLine
                        {
                            Id = 3,
                            GoodsIssueId = 2,
                            ProductId = 1, // Bút bi Thiên Long (Tồn hiện tại là 5)
                            Quantity = 10, // Xuất 10 > Tồn 5
                            UnitPrice = 5000
                        }
                    }
                },
                new GoodsIssue
                {
                    Id = 3,
                    IssueNumber = "PX-2026-003",
                    CustomerOrDepartment = "Khách bán lẻ Đại lý An Bình",
                    IssueDate = DateTime.Now.AddHours(-1),
                    CreatedBy = "Thủ kho Nguyễn Văn A",
                    ApprovedBy = null,
                    ApprovedDate = null,
                    Status = ReceiptStatus.Draft, // Chờ duyệt
                    Note = "Xuất hàng hợp lệ để kiểm tra duyệt thành công",
                    Lines = new List<GoodsIssueLine>
                    {
                        new GoodsIssueLine
                        {
                            Id = 4,
                            GoodsIssueId = 3,
                            ProductId = 2, // Mì Hảo Hảo (Tồn 80 >= 5)
                            Quantity = 5,
                            UnitPrice = 115000
                        }
                    }
                }
            };
        }

        public async Task<IEnumerable<GoodsIssue>> GetGoodsIssuesAsync(ReceiptStatus? status = null)
        {
            IEnumerable<GoodsIssue> query = _issues;
            if (status.HasValue)
            {
                query = query.Where(i => i.Status == status.Value);
            }

            var list = query.OrderByDescending(i => i.IssueDate).ToList();
            foreach (var issue in list)
            {
                foreach (var line in issue.Lines)
                {
                    line.Product = await _productRepository.GetProductByIdAsync(line.ProductId);
                }
            }

            return list;
        }

        public async Task<GoodsIssue?> GetGoodsIssueByIdAsync(int id)
        {
            var issue = _issues.FirstOrDefault(i => i.Id == id);
            if (issue != null)
            {
                foreach (var line in issue.Lines)
                {
                    line.Product = await _productRepository.GetProductByIdAsync(line.ProductId);
                }
            }
            return issue;
        }

        public Task<int> CreateGoodsIssueAsync(GoodsIssue issue)
        {
            lock (_issues)
            {
                int maxId = _issues.Any() ? _issues.Max(i => i.Id) : 0;
                issue.Id = maxId + 1;
                issue.IssueNumber = $"PX-2026-{(issue.Id):D3}";
                issue.IssueDate = DateTime.Now;
                issue.Status = ReceiptStatus.Draft;

                int lineId = 1;
                foreach (var line in issue.Lines)
                {
                    line.Id = lineId++;
                    line.GoodsIssueId = issue.Id;
                }

                _issues.Add(issue);
                return Task.FromResult(issue.Id);
            }
        }

        public async Task ApproveIssueTransactionAsync(int issueId, string approvedBy)
        {
            var issue = _issues.FirstOrDefault(i => i.Id == issueId);
            if (issue == null || issue.Status != ReceiptStatus.Draft) return;

            // Transaction: Đổi trạng thái thành Approved & Trừ kho
            lock (_issues)
            {
                issue.Status = ReceiptStatus.Approved;
                issue.ApprovedBy = approvedBy;
                issue.ApprovedDate = DateTime.Now;
            }

            foreach (var line in issue.Lines)
            {
                await _productRepository.UpdateStockAsync(line.ProductId, -line.Quantity);
            }
        }

        public Task CancelIssueAsync(int issueId)
        {
            var issue = _issues.FirstOrDefault(i => i.Id == issueId);
            if (issue != null && issue.Status == ReceiptStatus.Draft)
            {
                issue.Status = ReceiptStatus.Cancelled;
            }
            return Task.CompletedTask;
        }
    }
}
