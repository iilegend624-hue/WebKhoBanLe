using KhoBanLe.CoreBusiness;

namespace KhoBanLe.UseCases.PluginInterfaces.DataStore
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomersAsync(string? filter = null);
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task<Customer?> GetCustomerByPhoneAsync(string phone);
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
    }
}
