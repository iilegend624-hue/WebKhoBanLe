using KhoBanLe.CoreBusiness;
using KhoBanLe.UseCases.PluginInterfaces.DataStore;

namespace KhoBanLe.UseCases.Customers
{
    public interface IGetCustomersUseCase
    {
        Task<IEnumerable<Customer>> ExecuteAsync(string? filter = null);
    }

    public class GetCustomersUseCase : IGetCustomersUseCase
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomersUseCase(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<Customer>> ExecuteAsync(string? filter = null)
        {
            return await _customerRepository.GetCustomersAsync(filter);
        }
    }

    public interface IAddCustomerUseCase
    {
        Task ExecuteAsync(Customer customer);
    }

    public class AddCustomerUseCase : IAddCustomerUseCase
    {
        private readonly ICustomerRepository _customerRepository;

        public AddCustomerUseCase(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task ExecuteAsync(Customer customer)
        {
            await _customerRepository.AddCustomerAsync(customer);
        }
    }
}
