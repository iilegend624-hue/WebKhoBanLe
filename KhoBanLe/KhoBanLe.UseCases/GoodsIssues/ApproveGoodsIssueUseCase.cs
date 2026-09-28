using KhoBanLe.CoreBusiness;
using KhoBanLe.UseCases.PluginInterfaces.DataStore;

namespace KhoBanLe.UseCases.GoodsIssues
{
    public class ApproveGoodsIssueResult
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public string? ProductName { get; set; }
        public int CurrentStock { get; set; }
        public int RequestedQuantity { get; set; }

        public static ApproveGoodsIssueResult Ok() => new() { Success = true };

        public static ApproveGoodsIssueResult Fail(string message, string? productName = null, int currentStock = 0, int requestedQuantity = 0)
        {
            return new ApproveGoodsIssueResult
            {
                Success = false,
                ErrorMessage = message,
                ProductName = productName,
                CurrentStock = currentStock,
                RequestedQuantity = requestedQuantity
            };
        }
    }

    public interface IApproveGoodsIssueUseCase
    {
        Task<ApproveGoodsIssueResult> ExecuteAsync(int issueId, string approvedBy);
    }

    /// <summary>
    /// LUẬT 1: DUYỆT MỚI ĐỔI TỒN & CHẶN XUẤT ÂM
    /// Vị trí: KhoBanLe.UseCases.GoodsIssues.ApproveGoodsIssueUseCase
    /// - Tồn kho CHỈ thay đổi khi phiếu chuyển sang trạng thái Approved trong 1 transaction.
    /// - Phiếu xuất có số lượng vượt tồn kho hiện tại BẮT BUỘC BỊ CHẶN.
    /// - Hiển thị Alert thân thiện: 'Hàng [Tên] chỉ còn tồn [X], không đủ để xuất [Y]!' (Không văng exception).
    /// </summary>
    public class ApproveGoodsIssueUseCase : IApproveGoodsIssueUseCase
    {
        private readonly IGoodsIssueRepository _goodsIssueRepository;
        private readonly IProductRepository _productRepository;

        public ApproveGoodsIssueUseCase(
            IGoodsIssueRepository goodsIssueRepository,
            IProductRepository productRepository)
        {
            _goodsIssueRepository = goodsIssueRepository;
            _productRepository = productRepository;
        }

        public async Task<ApproveGoodsIssueResult> ExecuteAsync(int issueId, string approvedBy)
        {
            var issue = await _goodsIssueRepository.GetGoodsIssueByIdAsync(issueId);
            if (issue == null)
            {
                return ApproveGoodsIssueResult.Fail("Không tìm thấy phiếu xuất kho yêu cầu.");
            }

            if (issue.Status == ReceiptStatus.Approved)
            {
                return ApproveGoodsIssueResult.Fail("Phiếu xuất này đã được duyệt trước đó.");
            }

            if (issue.Status == ReceiptStatus.Cancelled)
            {
                return ApproveGoodsIssueResult.Fail("Phiếu xuất này đã bị hủy, không thể phê duyệt.");
            }

            // KIỂM TRA LUẬT 1: Chặn xuất âm kho trên từng dòng hàng hóa
            foreach (var line in issue.Lines)
            {
                var product = await _productRepository.GetProductByIdAsync(line.ProductId);
                if (product == null)
                {
                    return ApproveGoodsIssueResult.Fail($"Mặt hàng mã #{line.ProductId} không tồn tại trong hệ thống.");
                }

                if (product.CurrentStock < line.Quantity)
                {
                    // Chặn lại ngay lập tức, không trừ kho, trả về thông báo lỗi thân thiện chuẩn Slide 7
                    return ApproveGoodsIssueResult.Fail(
                        $"Hàng {product.Name} chỉ còn tồn {product.CurrentStock}, không đủ để xuất {line.Quantity}!",
                        product.Name,
                        product.CurrentStock,
                        line.Quantity);
                }
            }

            // Tất cả các dòng hợp lệ -> Tiến hành duyệt & trừ kho trong 1 transaction an toàn
            await _goodsIssueRepository.ApproveIssueTransactionAsync(issueId, approvedBy);

            return ApproveGoodsIssueResult.Ok();
        }
    }
}
