using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace KhoBanLe.Services
{
    /// <summary>
    /// Tiện ích tìm kiếm thông minh hỗ trợ:
    /// 1. Tìm theo ký tự đơn lẻ (vd: gõ 'h' -> ra Mì Hảo Hảo, Bút bi Thiên Long, Hóa mỹ phẩm...)
    /// 2. Tìm không dấu hoặc có dấu (vd: "hao hao" hoặc "Hảo Hảo")
    /// 3. Tìm dính liền không dấu cách (Compact Match: "haohao" -> "Mì Hảo Hảo", "cocacola" -> "Coca-Cola")
    /// 4. Tìm theo chữ cái viết tắt / Initials ("hh" -> "Hảo Hảo", "tl" -> "Thiên Long", "vm" -> "Vinamilk")
    /// 5. Tìm theo từng từ độc lập (Multi-token match: "mi hao", "sua 1l")
    /// </summary>
    public static class SearchHelper
    {
        public static string RemoveDiacritics(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;

            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder(capacity: normalizedString.Length);

            for (int i = 0; i < normalizedString.Length; i++)
            {
                char c = normalizedString[i];
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder
                .ToString()
                .Normalize(NormalizationForm.FormC)
                .Replace('đ', 'd')
                .Replace('Đ', 'D');
        }

        public static string ToCompact(string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;
            var sb = new StringBuilder(text.Length);
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (char.IsLetterOrDigit(c))
                {
                    sb.Append(char.ToLowerInvariant(c));
                }
            }
            return sb.ToString();
        }

        public static string GetInitials(string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;
            var sb = new StringBuilder();
            bool isWordStart = true;
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (char.IsLetterOrDigit(c))
                {
                    if (isWordStart)
                    {
                        sb.Append(char.ToLowerInvariant(c));
                        isWordStart = false;
                    }
                }
                else
                {
                    isWordStart = true;
                }
            }
            return sb.ToString();
        }

        public static bool Matches(string? source, string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return true;
            if (string.IsNullOrWhiteSpace(source)) return false;

            query = query.Trim();

            // 1. Khớp chuỗi trực tiếp (case-insensitive):
            // Vd: gõ ký tự đơn lẻ 'h', 'Hảo', '01', 'lon', 'thùng'...
            if (source.Contains(query, StringComparison.OrdinalIgnoreCase))
                return true;

            // 2. Chuẩn hóa bỏ dấu tiếng Việt (vd: "Mì Hảo Hảo" -> "Mi Hao Hao")
            string cleanSource = RemoveDiacritics(source);
            string cleanQuery = RemoveDiacritics(query);

            // 2a. Khớp chuỗi sau khi bỏ dấu (vd: gõ "h", "hao", "hao hao", "mi hao")
            if (cleanSource.Contains(cleanQuery, StringComparison.OrdinalIgnoreCase))
                return true;

            // 3. Khớp Compact (loại bỏ toàn bộ dấu cách, gạch nối, ký tự phân cách)
            // ĐẶC BIỆT QUAN TRỌNG: Gõ "haohao" -> khớp "Mi Hao Hao"
            // Gõ "cocacola" -> khớp "Coca-Cola"
            // Gõ "thienlong" -> khớp "Thiên Long"
            // Gõ "botgiat" -> khớp "Bột giặt"
            string compactSource = ToCompact(cleanSource);
            string compactQuery = ToCompact(cleanQuery);

            if (!string.IsNullOrEmpty(compactQuery) && compactSource.Contains(compactQuery, StringComparison.OrdinalIgnoreCase))
                return true;

            // 4. Khớp từng từ độc lập (Multi-token match: tất cả các từ trong query phải có trong source)
            // Vd: "mi tom hao hao", "vinamilk 1l", "omo matic"
            var queryWords = cleanQuery.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (queryWords.Length > 1)
            {
                bool allWordsMatch = true;
                foreach (var word in queryWords)
                {
                    string compactWord = ToCompact(word);
                    if (!string.IsNullOrEmpty(compactWord) && !compactSource.Contains(compactWord, StringComparison.OrdinalIgnoreCase))
                    {
                        allWordsMatch = false;
                        break;
                    }
                }
                if (allWordsMatch) return true;
            }

            // 5. Khớp chữ cái viết tắt / Initials / Acronym:
            // Vd: "hh" -> "Hảo Hảo"
            // "mhh" -> "Mì Hảo Hảo"
            // "tl" -> "Thiên Long"
            // "vm" -> "Vinamilk"
            // "cc" -> "Coca Cola"
            if (compactQuery.Length >= 2 && compactQuery.Length <= 6)
            {
                string initials = GetInitials(cleanSource);
                if (initials.Contains(compactQuery, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }
    }
}
