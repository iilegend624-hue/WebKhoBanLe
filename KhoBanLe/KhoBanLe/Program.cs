using KhoBanLe.Components;
using KhoBanLe.DataStore.HardCoded;
using KhoBanLe.DataStore.HardCoded.Sql;
using KhoBanLe.Services;
using KhoBanLe.UseCases.AI;
using KhoBanLe.UseCases.GoodsIssues;
using KhoBanLe.UseCases.GoodsReceipts;
using KhoBanLe.UseCases.PluginInterfaces.DataStore;
using KhoBanLe.UseCases.Products;
using KhoBanLe.UseCases.Reports;
using KhoBanLe.UseCases.Suppliers;

namespace KhoBanLe
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            // =========================================================================
            // 1. ĐĂNG KÝ DATASTORE PLUGINS (Clean Architecture: Composition Root)
            // Minh chứng K1.1, K1.2, K2.2, K2.3, K2.4 và câu hỏi Vấn đáp "Đổi Plugin Dữ Liệu"
            // =========================================================================
            builder.Services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();

            // Cấu hình lựa chọn Plugin: SQL Server qua Dapper (Mặc định) hoặc In-Memory (Kiểm thử offline)
            bool useSqlDataStore = builder.Configuration.GetValue<bool>("UseSqlDataStore", true);

            if (useSqlDataStore)
            {
                // [PLUGIN SQL SERVER - DAPPER]: Đáp ứng K2.2 (Transaction), K2.3 (Parameterized SQL), K2.4 (SQL Server SSMS)
                builder.Services.AddScoped<IProductRepository, SqlProductRepository>();
                builder.Services.AddScoped<ISupplierRepository, SqlSupplierRepository>();
                builder.Services.AddScoped<IGoodsReceiptRepository, SqlGoodsReceiptRepository>();
                builder.Services.AddScoped<IGoodsIssueRepository, SqlGoodsIssueRepository>();
                builder.Services.AddScoped<IInventoryReportRepository, SqlInventoryReportRepository>();
                builder.Services.AddScoped<ICustomerRepository, SqlCustomerRepository>();
                builder.Services.AddScoped<IPosOrderRepository, SqlPosOrderRepository>();
            }
            else
            {
                // [PLUGIN IN-MEMORY]: Đáp ứng Sprint 1 (Offline mock data)
                builder.Services.AddSingleton<IProductRepository, ProductRepository>();
                builder.Services.AddSingleton<ISupplierRepository, SupplierRepository>();
                builder.Services.AddSingleton<IGoodsReceiptRepository, GoodsReceiptRepository>();
                builder.Services.AddSingleton<IGoodsIssueRepository, GoodsIssueRepository>();
                builder.Services.AddSingleton<IInventoryReportRepository, InventoryReportRepository>();
                builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
                builder.Services.AddSingleton<IPosOrderRepository, PosOrderRepository>();
            }

            // =========================================================================
            // 2. ĐĂNG KÝ CÁC USE CASES NGHIỆP VỤ (Clean Architecture: Tầng UseCases)
            // Minh chứng K1.1 (UseCases chỉ tham chiếu CoreBusiness), K1.2 (Inject Interface)
            // =========================================================================
            // Products
            builder.Services.AddTransient<ISearchProductsUseCase, SearchProductsUseCase>();
            builder.Services.AddTransient<IViewProductUseCase, ViewProductUseCase>();
            builder.Services.AddTransient<IAddProductUseCase, AddProductUseCase>();
            builder.Services.AddTransient<IEditProductUseCase, EditProductUseCase>();
            builder.Services.AddTransient<IDeleteProductUseCase, DeleteProductUseCase>();

            // Suppliers
            builder.Services.AddTransient<IGetSuppliersUseCase, GetSuppliersUseCase>();

            // Customers
            builder.Services.AddTransient<KhoBanLe.UseCases.Customers.IGetCustomersUseCase, KhoBanLe.UseCases.Customers.GetCustomersUseCase>();
            builder.Services.AddTransient<KhoBanLe.UseCases.Customers.IAddCustomerUseCase, KhoBanLe.UseCases.Customers.AddCustomerUseCase>();

            // POS Orders (Bán Hàng Tại Quầy)
            builder.Services.AddTransient<KhoBanLe.UseCases.Pos.IGetPosOrdersUseCase, KhoBanLe.UseCases.Pos.GetPosOrdersUseCase>();
            builder.Services.AddTransient<KhoBanLe.UseCases.Pos.IViewPosOrderUseCase, KhoBanLe.UseCases.Pos.ViewPosOrderUseCase>();
            builder.Services.AddTransient<KhoBanLe.UseCases.Pos.ICreatePosOrderUseCase, KhoBanLe.UseCases.Pos.CreatePosOrderUseCase>();

            // Goods Receipts (Phiếu Nhập)
            builder.Services.AddTransient<ICreateGoodsReceiptUseCase, CreateGoodsReceiptUseCase>();
            builder.Services.AddTransient<IViewGoodsReceiptUseCase, ViewGoodsReceiptUseCase>();
            builder.Services.AddTransient<IApproveGoodsReceiptUseCase, ApproveGoodsReceiptUseCase>();
            builder.Services.AddTransient<ICancelGoodsReceiptUseCase, CancelGoodsReceiptUseCase>();

            // Goods Issues (Phiếu Xuất & LUẬT 1: Duyệt mới đổi tồn & Chặn xuất âm - K1.3)
            builder.Services.AddTransient<ICreateGoodsIssueUseCase, CreateGoodsIssueUseCase>();
            builder.Services.AddTransient<IViewGoodsIssueUseCase, ViewGoodsIssueUseCase>();
            builder.Services.AddTransient<IApproveGoodsIssueUseCase, ApproveGoodsIssueUseCase>();
            builder.Services.AddTransient<ICancelGoodsIssueUseCase, CancelGoodsIssueUseCase>();

            // Reports (Báo cáo NXT & LUẬT 2: Bảo toàn công thức Tồn đầu + Nhập - Xuất = Tồn cuối - K1.3)
            builder.Services.AddTransient<IGetInventoryReportUseCase, GetInventoryReportUseCase>();

            // AI SmartPaste (Mục 10 & K4.3: Trích xuất báo giá nhà cung cấp)
            builder.Services.AddTransient<IParseSupplierQuoteUseCase, ParseSupplierQuoteUseCase>();

            // =========================================================================
            // 3. ĐĂNG KÝ DỊCH VỤ WEB (TIÊU CHÍ K1.4: Giỏ/Lựa chọn tạm dùng service SCOPED)
            // =========================================================================
            builder.Services.AddScoped<DraftReceiptStateService>(); // Quản lý phiếu nháp (Scoped theo circuit người dùng)
            builder.Services.AddScoped<ShoppingCartService>();      // Quản lý giỏ hàng POS (Scoped theo circuit người dùng - Video Part 3)
            builder.Services.AddScoped<UserRoleService>();          // Quản lý phân quyền & phiên đăng nhập 2 vai trò (K4.1, K4.2)
            builder.Services.AddScoped<ExcelExportService>();          // Xuất file Excel / CSV UTF-8 (K4.3)

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
                app.UseHttpsRedirection();
            }
            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
