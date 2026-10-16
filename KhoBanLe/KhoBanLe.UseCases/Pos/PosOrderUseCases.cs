using KhoBanLe.CoreBusiness;
using KhoBanLe.UseCases.PluginInterfaces.DataStore;

namespace KhoBanLe.UseCases.Pos
{
    public interface IGetPosOrdersUseCase
    {
        Task<IEnumerable<PosOrder>> ExecuteAsync(DateTime? fromDate = null, DateTime? toDate = null);
    }

    public class GetPosOrdersUseCase : IGetPosOrdersUseCase
    {
        private readonly IPosOrderRepository _posOrderRepository;

        public GetPosOrdersUseCase(IPosOrderRepository posOrderRepository)
        {
            _posOrderRepository = posOrderRepository;
        }

        public async Task<IEnumerable<PosOrder>> ExecuteAsync(DateTime? fromDate = null, DateTime? toDate = null)
        {
            return await _posOrderRepository.GetPosOrdersAsync(fromDate, toDate);
        }
    }

    public interface IViewPosOrderUseCase
    {
        Task<PosOrder?> ExecuteAsync(int id);
        Task<PosOrder?> ExecuteByNumberAsync(string orderNumber);
    }

    public class ViewPosOrderUseCase : IViewPosOrderUseCase
    {
        private readonly IPosOrderRepository _posOrderRepository;

        public ViewPosOrderUseCase(IPosOrderRepository posOrderRepository)
        {
            _posOrderRepository = posOrderRepository;
        }

        public async Task<PosOrder?> ExecuteAsync(int id)
        {
            return await _posOrderRepository.GetPosOrderByIdAsync(id);
        }

        public async Task<PosOrder?> ExecuteByNumberAsync(string orderNumber)
        {
            return await _posOrderRepository.GetPosOrderByNumberAsync(orderNumber);
        }
    }

    public interface ICreatePosOrderUseCase
    {
        Task<int> ExecuteAsync(PosOrder order);
    }

    public class CreatePosOrderUseCase : ICreatePosOrderUseCase
    {
        private readonly IPosOrderRepository _posOrderRepository;

        public CreatePosOrderUseCase(IPosOrderRepository posOrderRepository)
        {
            _posOrderRepository = posOrderRepository;
        }

        public async Task<int> ExecuteAsync(PosOrder order)
        {
            return await _posOrderRepository.CreatePosOrderAsync(order);
        }
    }

    public interface ICancelPosOrderUseCase
    {
        Task<bool> ExecuteAsync(int id, string reason = "");
    }

    public class CancelPosOrderUseCase : ICancelPosOrderUseCase
    {
        private readonly IPosOrderRepository _posOrderRepository;

        public CancelPosOrderUseCase(IPosOrderRepository posOrderRepository)
        {
            _posOrderRepository = posOrderRepository;
        }

        public async Task<bool> ExecuteAsync(int id, string reason = "")
        {
            return await _posOrderRepository.CancelPosOrderAsync(id, reason);
        }
    }
}
