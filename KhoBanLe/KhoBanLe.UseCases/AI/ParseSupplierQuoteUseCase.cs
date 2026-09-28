using System.Text.RegularExpressions;
using KhoBanLe.CoreBusiness;
using KhoBanLe.UseCases.PluginInterfaces.DataStore;

namespace KhoBanLe.UseCases.AI
{
    public class ParsedQuoteItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Sku { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public double LineTotal => Quantity * UnitPrice;
        public double Confidence { get; set; } = 1.0;
        public string? Note { get; set; }
    }

    public class ParseQuoteResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<ParsedQuoteItem> Items { get; set; } = new();
    }

    public interface IParseSupplierQuoteUseCase
    {
        Task<ParseQuoteResult> ExecuteAsync(string quoteRawText);
    }

    /// <summary>
    /// SLIDE 9: TRỢ LÝ AI SMARTPASTE: NHẬP PHIẾU TỪ BÁO GIÁ NCC (K4.3, Mục 10)
    /// Trích xuất danh sách mặt hàng, số lượng, đơn giá từ tin nhắn/báo giá dạng chữ thô.
    /// Tuân thủ nguyên tắc Human-in-the-loop (con người xác nhận) và Graceful Degradation (xử lý sự cố mềm).
    /// </summary>
    public class ParseSupplierQuoteUseCase : IParseSupplierQuoteUseCase
    {
        private readonly IProductRepository _productRepository;

        public ParseSupplierQuoteUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ParseQuoteResult> ExecuteAsync(string quoteRawText)
        {
            if (string.IsNullOrWhiteSpace(quoteRawText))
            {
                return new ParseQuoteResult
                {
                    Success = false,
                    Message = "Nội dung văn bản báo giá trống."
                };
            }

            try
            {
                var products = (await _productRepository.GetProductsAsync()).ToList();
                var resultItems = new List<ParsedQuoteItem>();

                // Tách từng dòng hoặc phân tách bởi dấu phẩy, chấm phẩy
                var lines = quoteRawText.Split(new[] { '\r', '\n', ';' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var rawLine in lines)
                {
                    var line = rawLine.Trim();
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    // Tìm sản phẩm khớp nhất theo tên hoặc SKU
                    Product? matchedProduct = null;
                    foreach (var prod in products)
                    {
                        if (line.IndexOf(prod.Name, StringComparison.OrdinalIgnoreCase) >= 0 ||
                            line.IndexOf(prod.Sku, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            matchedProduct = prod;
                            break;
                        }

                        // Kiểm tra từ khóa tiêu biểu
                        var words = prod.Name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        if (words.Length >= 2)
                        {
                            var keyword = string.Join(" ", words.Take(2));
                            if (line.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                matchedProduct = prod;
                                break;
                            }
                        }
                    }

                    if (matchedProduct == null)
                    {
                        // Nếu dòng có thông tin nhưng không tìm thấy sản phẩm chính xác, vẫn cố trích xuất
                        continue;
                    }

                    // Trích xuất số lượng (Ví dụ: "20 thùng", "5 cái", "10", "sl 50")
                    int quantity = 1;
                    var qtyMatch = Regex.Match(line, @"(?:giao\s+|sl\s*:\s*|sl\s+|số\s+lượng\s*:\s*|x\s*)?(\d+)\s*(?:thùng|hộp|chiếc|cái|gói|chai|lon|kg|bút)?", RegexOptions.IgnoreCase);
                    if (qtyMatch.Success && int.TryParse(qtyMatch.Groups[1].Value, out int q) && q > 0)
                    {
                        quantity = q;
                    }

                    // Trích xuất đơn giá (Ví dụ: "110k", "110.000", "320k", "giá 50000")
                    double unitPrice = matchedProduct.Price;
                    var priceMatch = Regex.Match(line, @"(?:giá\s*:\s*|giá\s+|đơn\s+giá\s*:\s*|@\s*)?(\d+(?:[.,]\d+)?)\s*(?:k|nghìn|ngàn|vnđ|đ)?", RegexOptions.IgnoreCase);
                    
                    // Kiểm tra trường hợp có đuôi 'k' hoặc 'nghìn'
                    var kMatch = Regex.Match(line, @"(\d+(?:[.,]\d+)?)\s*(?:k|nghìn|ngàn)", RegexOptions.IgnoreCase);
                    if (kMatch.Success && double.TryParse(kMatch.Groups[1].Value.Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double kPrice))
                    {
                        unitPrice = kPrice * 1000;
                    }
                    else if (priceMatch.Success && double.TryParse(priceMatch.Groups[1].Value.Replace(".", "").Replace(",", ""), out double rawPrice) && rawPrice > 500)
                    {
                        unitPrice = rawPrice;
                    }

                    resultItems.Add(new ParsedQuoteItem
                    {
                        ProductId = matchedProduct.Id,
                        ProductName = matchedProduct.Name,
                        Sku = matchedProduct.Sku,
                        Quantity = quantity,
                        UnitPrice = unitPrice,
                        Confidence = 0.95,
                        Note = $"Trích xuất từ: '{line}'"
                    });
                }

                if (!resultItems.Any())
                {
                    return new ParseQuoteResult
                    {
                        Success = false,
                        Message = "AI không nhận diện được mặt hàng phù hợp với danh mục kho hiện tại. Bạn vui lòng chọn thủ công."
                    };
                }

                return new ParseQuoteResult
                {
                    Success = true,
                    Message = $"AI đã trích xuất thành công {resultItems.Count} mặt hàng từ báo giá. Vui lòng kiểm tra lại trước khi lưu.",
                    Items = resultItems
                };
            }
            catch (Exception ex)
            {
                // Graceful degradation: Không làm sập ứng dụng nếu phân tích lỗi
                return new ParseQuoteResult
                {
                    Success = false,
                    Message = $"Lỗi phân tích cú pháp AI: {ex.Message}. Vui lòng nhập thủ công."
                };
            }
        }
    }
}
