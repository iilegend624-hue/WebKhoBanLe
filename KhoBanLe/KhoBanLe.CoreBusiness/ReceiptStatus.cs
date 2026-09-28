namespace KhoBanLe.CoreBusiness
{
    public enum ReceiptStatus
    {
        Draft = 0,     // Chờ duyệt / Nháp
        Approved = 1,  // Đã duyệt (Cập nhật tồn kho)
        Cancelled = 2  // Đã hủy
    }
}
