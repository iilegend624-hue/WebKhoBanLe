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

    public interface IViewSupplierUseCase
    {
        Task<Supplier?> ExecuteAsync(int id);
    }

    public class ViewSupplierUseCase : IViewSupplierUseCase
    {
        private readonly ISupplierRepository _supplierRepository;

        public ViewSupplierUseCase(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<Supplier?> ExecuteAsync(int id)
        {
            return await _supplierRepository.GetSupplierByIdAsync(id);
        }
    }

    public interface IAddSupplierUseCase
    {
        Task ExecuteAsync(Supplier supplier);
    }

    public class AddSupplierUseCase : IAddSupplierUseCase
    {
        private readonly ISupplierRepository _supplierRepository;

        public AddSupplierUseCase(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task ExecuteAsync(Supplier supplier)
        {
            await _supplierRepository.AddSupplierAsync(supplier);
        }
    }

    public interface IEditSupplierUseCase
    {
        Task ExecuteAsync(Supplier supplier);
    }

    public class EditSupplierUseCase : IEditSupplierUseCase
    {
        private readonly ISupplierRepository _supplierRepository;

        public EditSupplierUseCase(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task ExecuteAsync(Supplier supplier)
        {
            await _supplierRepository.UpdateSupplierAsync(supplier);
        }
    }

    public class DeleteSupplierResult
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }

        public static DeleteSupplierResult Ok() => new() { Success = true };
        public static DeleteSupplierResult Fail(string msg) => new() { Success = false, ErrorMessage = msg };
    }

    public interface IDeleteSupplierUseCase
    {
        Task<DeleteSupplierResult> ExecuteAsync(int id);
    }

    public class DeleteSupplierUseCase : IDeleteSupplierUseCase
    {
        private readonly ISupplierRepository _supplierRepository;

        public DeleteSupplierUseCase(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<DeleteSupplierResult> ExecuteAsync(int id)
        {
            var hasReceipts = await _supplierRepository.HasGoodsReceiptsAsync(id);
            if (hasReceipts)
            {
                return DeleteSupplierResult.Fail("Nhà cung cấp này đã phát sinh phiếu nhập kho trong hệ thống, không thể xóa để bảo toàn tính toàn vẹn và lịch sử chứng từ!");
            }

            await _supplierRepository.DeleteSupplierAsync(id);
            return DeleteSupplierResult.Ok();
        }
    }
}
