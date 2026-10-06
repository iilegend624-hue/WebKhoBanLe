using KhoBanLe.CoreBusiness;
using KhoBanLe.UseCases.PluginInterfaces.DataStore;

namespace KhoBanLe.DataStore.HardCoded
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly List<Supplier> _suppliers;

        public SupplierRepository()
        {
            _suppliers = new List<Supplier>
            {
                new Supplier
                {
                    Id = 1,
                    SupplierCode = "NCC-TL",
                    Name = "Công ty CP Tập đoàn Thiên Long",
                    Phone = "028 3750 5555",
                    Email = "sales@thienlong.vn",
                    Address = "Tầng 10, Sofic Tower, 10 Mai Chí Thọ, TP. Thủ Đức, TP.HCM",
                    IsActive = true
                },
                new Supplier
                {
                    Id = 2,
                    SupplierCode = "NCC-VNM",
                    Name = "Công ty CP Sữa Việt Nam (Vinamilk)",
                    Phone = "028 5415 5555",
                    Email = "vinamilk@vinamilk.com.vn",
                    Address = "Số 10 Tân Trào, Phường Tân Phú, Quận 7, TP.HCM",
                    IsActive = true
                },
                new Supplier
                {
                    Id = 3,
                    SupplierCode = "NCC-AC",
                    Name = "Công ty CP Acecook Việt Nam",
                    Phone = "028 3815 4064",
                    Email = "info@acecookvietnam.com",
                    Address = "Lô II-3, Đường số 11, KCN Tân Bình, P. Tây Thạnh, Tân Phú, TP.HCM",
                    IsActive = true
                },
                new Supplier
                {
                    Id = 4,
                    SupplierCode = "NCC-KO",
                    Name = "Công ty TNHH NGK Coca-Cola Việt Nam",
                    Phone = "028 3896 1000",
                    Email = "cskh@coca-cola.com.vn",
                    Address = "Xa lộ Hà Nội, P. Linh Trung, TP. Thủ Đức, TP.HCM",
                    IsActive = true
                }
            };
        }

        public Task<IEnumerable<Supplier>> GetSuppliersAsync()
        {
            return Task.FromResult(_suppliers.Where(s => s.IsActive).AsEnumerable());
        }

        public Task<Supplier?> GetSupplierByIdAsync(int id)
        {
            var supplier = _suppliers.FirstOrDefault(s => s.Id == id);
            return Task.FromResult(supplier);
        }

        public Task AddSupplierAsync(Supplier supplier)
        {
            lock (_suppliers)
            {
                int maxId = _suppliers.Any() ? _suppliers.Max(s => s.Id) : 0;
                supplier.Id = maxId + 1;
                _suppliers.Add(supplier);
            }
            return Task.CompletedTask;
        }

        public Task UpdateSupplierAsync(Supplier supplier)
        {
            lock (_suppliers)
            {
                var existing = _suppliers.FirstOrDefault(s => s.Id == supplier.Id);
                if (existing != null)
                {
                    existing.SupplierCode = supplier.SupplierCode;
                    existing.Name = supplier.Name;
                    existing.Phone = supplier.Phone;
                    existing.Email = supplier.Email;
                    existing.Address = supplier.Address;
                    existing.IsActive = supplier.IsActive;
                }
            }
            return Task.CompletedTask;
        }
    }
}
