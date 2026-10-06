using KhoBanLe.CoreBusiness;
using KhoBanLe.UseCases.PluginInterfaces.DataStore;

namespace KhoBanLe.DataStore.HardCoded
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly List<Customer> _customers;

        public CustomerRepository()
        {
            _customers = new List<Customer>
            {
                new Customer
                {
                    Id = 1,
                    CustomerCode = "KH-001",
                    Name = "Khách mua lẻ tại quầy",
                    Phone = "0900 000 000",
                    Email = "khachle@khobanle.vn",
                    Address = "Mua trực tiếp tại quầy",
                    Points = 0,
                    CreatedAt = DateTime.Now.AddMonths(-2)
                },
                new Customer
                {
                    Id = 2,
                    CustomerCode = "KH-002",
                    Name = "Anh Hoàng Long",
                    Phone = "0903 123 456",
                    Email = "hoanglong@gmail.com",
                    Address = "120 Nguyễn Huệ, Phường Bến Nghé, Quận 1, TP.HCM",
                    Points = 120,
                    CreatedAt = DateTime.Now.AddMonths(-1)
                },
                new Customer
                {
                    Id = 3,
                    CustomerCode = "KH-003",
                    Name = "Chị Mai Phương",
                    Phone = "0918 654 321",
                    Email = "maiphuong@gmail.com",
                    Address = "45 Điện Biên Phủ, Phường 15, Bình Thạnh, TP.HCM",
                    Points = 450,
                    CreatedAt = DateTime.Now.AddMonths(-1)
                }
            };
        }

        public Task<IEnumerable<Customer>> GetCustomersAsync(string? filter = null)
        {
            IEnumerable<Customer> query = _customers;
            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(c => c.Name.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                                         c.Phone.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                                         c.CustomerCode.Contains(filter, StringComparison.OrdinalIgnoreCase));
            }
            return Task.FromResult(query);
        }

        public Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return Task.FromResult(_customers.FirstOrDefault(c => c.Id == id));
        }

        public Task<Customer?> GetCustomerByPhoneAsync(string phone)
        {
            return Task.FromResult(_customers.FirstOrDefault(c => c.Phone == phone));
        }

        public Task AddCustomerAsync(Customer customer)
        {
            lock (_customers)
            {
                int maxId = _customers.Any() ? _customers.Max(c => c.Id) : 0;
                customer.Id = maxId + 1;
                _customers.Add(customer);
            }
            return Task.CompletedTask;
        }

        public Task UpdateCustomerAsync(Customer customer)
        {
            lock (_customers)
            {
                var existing = _customers.FirstOrDefault(c => c.Id == customer.Id);
                if (existing != null)
                {
                    existing.Name = customer.Name;
                    existing.Phone = customer.Phone;
                    existing.Email = customer.Email;
                    existing.Address = customer.Address;
                    existing.Points = customer.Points;
                }
            }
            return Task.CompletedTask;
        }
    }
}
