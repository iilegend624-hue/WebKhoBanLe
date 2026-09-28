using KhoBanLe.CoreBusiness;
using KhoBanLe.UseCases.PluginInterfaces.DataStore;

namespace KhoBanLe.UseCases.GoodsReceipts
{
    public interface ICreateGoodsReceiptUseCase
    {
        Task<int> ExecuteAsync(GoodsReceipt receipt);
    }

    public class CreateGoodsReceiptUseCase : ICreateGoodsReceiptUseCase
    {
        private readonly IGoodsReceiptRepository _receiptRepository;

        public CreateGoodsReceiptUseCase(IGoodsReceiptRepository receiptRepository)
        {
            _receiptRepository = receiptRepository;
        }

        public async Task<int> ExecuteAsync(GoodsReceipt receipt)
        {
            receipt.Status = ReceiptStatus.Draft;
            return await _receiptRepository.CreateGoodsReceiptAsync(receipt);
        }
    }

    public interface IViewGoodsReceiptUseCase
    {
        Task<IEnumerable<GoodsReceipt>> ExecuteListAsync(ReceiptStatus? status = null);
        Task<GoodsReceipt?> ExecuteByIdAsync(int id);
    }

    public class ViewGoodsReceiptUseCase : IViewGoodsReceiptUseCase
    {
        private readonly IGoodsReceiptRepository _receiptRepository;

        public ViewGoodsReceiptUseCase(IGoodsReceiptRepository receiptRepository)
        {
            _receiptRepository = receiptRepository;
        }

        public async Task<IEnumerable<GoodsReceipt>> ExecuteListAsync(ReceiptStatus? status = null)
        {
            return await _receiptRepository.GetGoodsReceiptsAsync(status);
        }

        public async Task<GoodsReceipt?> ExecuteByIdAsync(int id)
        {
            return await _receiptRepository.GetGoodsReceiptByIdAsync(id);
        }
    }

    public interface IApproveGoodsReceiptUseCase
    {
        Task ExecuteAsync(int receiptId, string approvedBy);
    }

    public class ApproveGoodsReceiptUseCase : IApproveGoodsReceiptUseCase
    {
        private readonly IGoodsReceiptRepository _receiptRepository;

        public ApproveGoodsReceiptUseCase(IGoodsReceiptRepository receiptRepository)
        {
            _receiptRepository = receiptRepository;
        }

        public async Task ExecuteAsync(int receiptId, string approvedBy)
        {
            await _receiptRepository.ApproveReceiptTransactionAsync(receiptId, approvedBy);
        }
    }

    public interface ICancelGoodsReceiptUseCase
    {
        Task ExecuteAsync(int receiptId);
    }

    public class CancelGoodsReceiptUseCase : ICancelGoodsReceiptUseCase
    {
        private readonly IGoodsReceiptRepository _receiptRepository;

        public CancelGoodsReceiptUseCase(IGoodsReceiptRepository receiptRepository)
        {
            _receiptRepository = receiptRepository;
        }

        public async Task ExecuteAsync(int receiptId)
        {
            await _receiptRepository.CancelReceiptAsync(receiptId);
        }
    }
}
