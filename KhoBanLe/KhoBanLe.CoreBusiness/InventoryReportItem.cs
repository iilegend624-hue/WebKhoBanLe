namespace KhoBanLe.CoreBusiness
{
    /// <summary>
    /// LUẬT 2: BẢO TOÀN CÔNG THỨC NHẬP - XUẤT - TỒN
    /// Tồn đầu kỳ + Tổng nhập (Approved) - Tổng xuất (Approved) = Tồn cuối kỳ
    /// </summary>
    public class InventoryReportItem
    {
        public int ProductId { get; set; }
        public string Sku { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string Unit { get; set; } = "Cái";
        public string Category { get; set; } = string.Empty;

        public int BeginningStock { get; set; }
        public int TotalImport { get; set; }
        public int TotalExport { get; set; }

        // Công thức bất biến: Tồn cuối kỳ = Tồn đầu kỳ + Tổng nhập - Tổng xuất
        public int EndingStock => BeginningStock + TotalImport - TotalExport;

        // Tồn thực tế trong bảng Product để kiểm tra đối soát chéo độc lập
        public int CurrentStock { get; set; }

        // Kiểm tra đối soát: Kết quả tính toán từ chứng từ có khớp 100% với tồn kho thực tế?
        public bool IsBalanced => EndingStock == CurrentStock;

        public int MinStockLevel { get; set; }

        public double TotalImportValue { get; set; }
        public double TotalExportValue { get; set; }
        public double EndingStockValue { get; set; }
    }
}
