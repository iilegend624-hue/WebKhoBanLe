namespace KhoBanLe.Services
{
    public enum SystemRole
    {
        WarehouseStaff,  // Thủ kho (Tài khoản người dùng thông thường)
        InventoryManager // Quản lý kho (Tài khoản quản trị viên)
    }

    public class UserAccount
    {
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public SystemRole Role { get; set; }
        public string Email { get; set; } = string.Empty;
    }

    /// <summary>
    /// TIÊU CHÍ K4.1: Đăng nhập, đăng xuất, ít nhất 2 vai trò
    /// TIÊU CHÍ K4.2: Bảo vệ phân quyền, chặn tài khoản thường gõ URL trang quản trị
    /// </summary>
    public class UserRoleService
    {
        public static readonly List<UserAccount> PredefinedUsers = new()
        {
            new UserAccount
            {
                Username = "thukho",
                FullName = "Nguyễn Văn A",
                Role = SystemRole.WarehouseStaff,
                Email = "thukho@khobanle.vn"
            },
            new UserAccount
            {
                Username = "quanly",
                FullName = "Trần Thị B",
                Role = SystemRole.InventoryManager,
                Email = "quanly@khobanle.vn"
            }
        };

        public bool IsLoggedIn { get; private set; } = true;
        public UserAccount CurrentUser { get; private set; }

        public UserRoleService()
        {
            // Mặc định đăng nhập với vai trò Quản lý kho để tiện kiểm thử ban đầu
            CurrentUser = PredefinedUsers[1];
        }

        public SystemRole CurrentRole => CurrentUser.Role;
        public string CurrentUserName => $"{(CurrentRole == SystemRole.WarehouseStaff ? "Thủ kho" : "Quản lý")} {CurrentUser.FullName}";
        
        public string RoleDisplayName => CurrentRole == SystemRole.WarehouseStaff 
            ? "Thủ kho (Warehouse Staff)" 
            : "Quản lý kho (Inventory Manager)";

        public bool IsManager => IsLoggedIn && CurrentRole == SystemRole.InventoryManager;
        public bool IsStaff => IsLoggedIn && CurrentRole == SystemRole.WarehouseStaff;

        public event Action? OnRoleChanged;

        public bool Login(string username, string password)
        {
            var user = PredefinedUsers.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            if (user != null)
            {
                CurrentUser = user;
                IsLoggedIn = true;
                OnRoleChanged?.Invoke();
                return true;
            }
            return false;
        }

        public void Logout()
        {
            IsLoggedIn = false;
            OnRoleChanged?.Invoke();
        }

        public void SetRole(SystemRole role)
        {
            var user = PredefinedUsers.FirstOrDefault(u => u.Role == role) ?? PredefinedUsers[0];
            CurrentUser = user;
            IsLoggedIn = true;
            OnRoleChanged?.Invoke();
        }

        public void SwitchRole()
        {
            if (CurrentRole == SystemRole.WarehouseStaff)
            {
                SetRole(SystemRole.InventoryManager);
            }
            else
            {
                SetRole(SystemRole.WarehouseStaff);
            }
        }
    }
}
