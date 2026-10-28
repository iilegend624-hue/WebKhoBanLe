using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Markdig;

namespace ReportGen
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            string rootDir = @"c:\Users\trung\OneDrive\Máy tính\WebKhoBanLe";
            string mdPath = Path.Combine(rootDir, "baocao_khobanle.md");
            string htmlPath = Path.Combine(rootDir, "báo cáo kho bán lẻ.html");
            string docxPath = Path.Combine(rootDir, "báo cáo kho bán lẻ.docx");
            string pdfPath = Path.Combine(rootDir, "báo cáo kho bán lẻ.pdf");
            string logoPath = Path.Combine(rootDir, "logo_hce.png");

            Console.WriteLine($">>> Đang đọc file: {mdPath} ...");
            if (!File.Exists(mdPath))
            {
                Console.WriteLine("Lỗi: Không tìm thấy file baocao_khobanle.md!");
                return;
            }

            string mdContent = File.ReadAllText(mdPath, Encoding.UTF8);

            // Đọc logo base64
            string logoBase64 = "";
            if (File.Exists(logoPath))
            {
                byte[] logoBytes = File.ReadAllBytes(logoPath);
                logoBase64 = Convert.ToBase64String(logoBytes);
            }

            // Cấu hình Markdig với Advanced Extensions (Tables, TaskLists, etc.)
            var pipeline = new MarkdownPipelineBuilder()
                .UseAdvancedExtensions()
                .Build();

            Console.WriteLine(">>> Đang chuyển đổi Markdown sang HTML chuẩn in ấn ...");
            string bodyHtml = Markdown.ToHtml(mdContent, pipeline);

            // Bỏ tiêu đề H1 và H2 đầu trang markdown vì đã có ở trang bìa
            bodyHtml = Regex.Replace(bodyHtml, @"^<h1>BÁO CÁO THỰC TẬP NGHỀ NGHIỆP</h1>\s*", "", RegexOptions.Multiline);
            bodyHtml = Regex.Replace(bodyHtml, @"^<h1>XÂY DỰNG HỆ THỐNG QUẢN LÝ KHO BÁN LẺ &amp; BÁN HÀNG TẠI QUẦY \(POS\)</h1>\s*", "", RegexOptions.Multiline);
            bodyHtml = Regex.Replace(bodyHtml, @"^<h2>WEB KHOBANLE — KIẾN TRÚC CLEAN ARCHITECTURE 4 TẦNG &amp; MICROSOFT SQL SERVER</h2>\s*", "", RegexOptions.Multiline);

            // Thêm ngắt trang trước mỗi CHƯƠNG và các phần quan trọng
            bodyHtml = Regex.Replace(bodyHtml, @"<h2>LỜI CẢM ƠN</h2>", @"<div style=""page-break-before: always;""></div><h2>LỜI CẢM ƠN</h2>");
            bodyHtml = Regex.Replace(bodyHtml, @"<h2>MỤC LỤC TỔNG QUAN</h2>", @"<div style=""page-break-before: always;""></div><h2>MỤC LỤC TỔNG QUAN</h2>");
            bodyHtml = Regex.Replace(bodyHtml, @"<h2>DANH MỤC CÁC KÝ HIỆU VÀ TỪ VIẾT TẮT</h2>", @"<div style=""page-break-before: always;""></div><h2>DANH MỤC CÁC KÝ HIỆU VÀ TỪ VIẾT TẮT</h2>");
            bodyHtml = Regex.Replace(bodyHtml, @"<h2>CHƯƠNG ", @"<div style=""page-break-before: always;""></div><h2>CHƯƠNG ");
            bodyHtml = Regex.Replace(bodyHtml, @"<h2>TÀI LIỆU THAM KHẢO</h2>", @"<div style=""page-break-before: always;""></div><h2>TÀI LIỆU THAM KHẢO</h2>");

            // Bọc bảng biểu với class định dạng chuẩn Word
            bodyHtml = bodyHtml.Replace("<table>", "<table class=\"doc-table\" border=\"1\" cellspacing=\"0\" cellpadding=\"5\">");

            // Xây dựng trang HTML hoàn chỉnh tương thích Word HTML MSO
            string fullHtml = $@"<!DOCTYPE html>
<html xmlns:o='urn:schemas-microsoft-com:office:office' 
      xmlns:w='urn:schemas-microsoft-com:office:word' 
      xmlns='http://www.w3.org/TR/REC-html40'>
<head>
    <meta charset=""utf-8"">
    <title>Báo Cáo Thực Tập Nghề Nghiệp - Kho Bán Lẻ</title>
    <!--[if gte mso 9]>
    <xml>
        <w:WordDocument>
            <w:View>Print</w:View>
            <w:Zoom>100</w:Zoom>
            <w:DoNotOptimizeForBrowser/>
        </w:WordDocument>
    </xml>
    <![endif]-->
    <style>
        @page {{
            size: A4 portrait;
            margin: 20mm 20mm 20mm 25mm; /* Chuẩn: Trái 25mm đóng gáy, Trên/Dưới/Phải 20mm */
            mso-header-margin: 10mm;
            mso-footer-margin: 10mm;
        }}

        body {{
            font-family: 'Times New Roman', Times, serif;
            font-size: 13pt;
            line-height: 1.35;
            color: #111827;
            background-color: #ffffff;
            margin: 0;
            padding: 0;
            text-align: justify;
        }}

        /* TRANG BÌA CHUẨN ĐẠI HỌC KINH TẾ HUẾ */
        .cover-page {{
            text-align: center;
            padding: 24px 20px 20px 20px;
            border: 4px double #1e3a8a;
            border-radius: 4px;
            margin-bottom: 25px;
            page-break-after: always;
            min-height: 940px;
            box-sizing: border-box;
        }}
        .cover-univ {{
            font-size: 14pt;
            font-weight: bold;
            color: #111827;
            margin-bottom: 4px;
            text-transform: uppercase;
            letter-spacing: 0.05em;
        }}
        .cover-school {{
            font-size: 14pt;
            font-weight: bold;
            color: #1e3a8a;
            margin-bottom: 4px;
            text-transform: uppercase;
            letter-spacing: 0.05em;
        }}
        .cover-dept {{
            font-size: 13pt;
            font-weight: bold;
            color: #374151;
            margin-bottom: 25px;
            text-transform: uppercase;
            border-bottom: 1.5px solid #1e3a8a;
            display: inline-block;
            padding-bottom: 6px;
        }}
        .cover-logo-box {{
            margin: 20px auto 25px auto;
            text-align: center;
        }}
        .cover-logo {{
            width: 140px;
            height: 140px;
            object-fit: contain;
        }}
        .cover-report-type {{
            font-size: 18pt;
            font-weight: bold;
            color: #111827;
            margin-bottom: 14px;
            text-transform: uppercase;
            letter-spacing: 0.05em;
        }}
        .cover-title {{
            font-size: 17pt;
            font-weight: bold;
            color: #1e3a8a;
            margin: 10px 0 35px 0;
            text-transform: uppercase;
            line-height: 1.35;
        }}
        .cover-meta-table {{
            width: 88%;
            margin: 30px auto 35px auto;
            border: none;
            font-size: 13pt;
            text-align: left;
            line-height: 1.8;
        }}
        .cover-meta-table td {{
            border: none;
            padding: 4px 6px;
            vertical-align: top;
        }}
        .cover-meta-label {{
            width: 44%;
            font-weight: bold;
            color: #1f2937;
        }}
        .cover-meta-value {{
            font-weight: bold;
            color: #111827;
        }}
        .cover-footer {{
            font-size: 13pt;
            font-weight: bold;
            color: #374151;
            margin-top: 40px;
        }}

        /* TIÊU ĐỀ CHƯƠNG & MỤC */
        h1 {{
            font-size: 16pt;
            font-weight: bold;
            color: #1e3a8a;
            margin-top: 20px;
            margin-bottom: 10px;
            text-align: center;
            text-transform: uppercase;
            page-break-after: avoid;
        }}

        h2 {{
            font-size: 14.5pt;
            font-weight: bold;
            color: #1e3a8a;
            margin-top: 24px;
            margin-bottom: 10px;
            padding-bottom: 4px;
            border-bottom: 1.5px solid #1e3a8a;
            text-transform: uppercase;
            page-break-after: avoid;
        }}

        h3 {{
            font-size: 13.5pt;
            font-weight: bold;
            color: #1e40af;
            margin-top: 16px;
            margin-bottom: 6px;
            page-break-after: avoid;
        }}

        h4 {{
            font-size: 13pt;
            font-weight: bold;
            font-style: italic;
            color: #374151;
            margin-top: 12px;
            margin-bottom: 4px;
            page-break-after: avoid;
        }}

        p {{
            margin: 0 0 8px 0;
            text-indent: 1.25cm;
        }}

        /* BẢNG BIỂU CHUẨN BÁO CÁO */
        table.doc-table {{
            width: 100%;
            border-collapse: collapse;
            margin: 12px 0;
            font-size: 11pt;
            border: 1px solid #cbd5e1;
            page-break-inside: avoid;
        }}

        table.doc-table th {{
            background-color: #1e3a8a;
            color: #ffffff;
            font-weight: bold;
            text-align: center;
            padding: 6px 8px;
            border: 1px solid #1e3a8a;
        }}

        table.doc-table td {{
            padding: 5px 8px;
            border: 1px solid #cbd5e1;
            vertical-align: middle;
        }}

        table.doc-table tr:nth-child(even) {{
            background-color: #f8fafc;
        }}

        /* MÃ NGUỒN */
        pre {{
            background-color: #f8fafc;
            border: 1px solid #e2e8f0;
            border-left: 4px solid #2563eb;
            border-radius: 4px;
            padding: 8px 12px;
            margin: 10px 0;
            font-family: 'Consolas', 'Courier New', monospace;
            font-size: 9.5pt;
            line-height: 1.3;
            white-space: pre-wrap;
            word-break: break-all;
            page-break-inside: avoid;
        }}

        code {{
            font-family: 'Consolas', 'Courier New', monospace;
            font-size: 10.5pt;
            background-color: #f1f5f9;
            color: #b91c1c;
            padding: 1px 3px;
            border-radius: 3px;
        }}

        pre code {{
            background-color: transparent;
            color: #1f2937;
            padding: 0;
            border: none;
            font-size: 9.5pt;
        }}

        /* TRÍCH DẪN & GHI CHÚ */
        blockquote {{
            margin: 10px 0;
            padding: 8px 14px;
            background-color: #eff6ff;
            border-left: 4px solid #3b82f6;
            border-radius: 3px;
            font-size: 11.5pt;
            page-break-inside: avoid;
        }}

        blockquote p {{
            margin: 0;
            text-indent: 0;
        }}

        ul, ol {{
            margin: 6px 0 10px 24px;
            padding: 0;
        }}

        li {{
            margin-bottom: 4px;
            line-height: 1.35;
        }}

        hr {{
            border: 0;
            height: 1px;
            background-color: #e2e8f0;
            margin: 18px 0;
        }}

        a {{
            color: #1e3a8a;
            text-decoration: none;
        }}
    </style>
</head>
<body>

    <!-- TRANG BÌA CHUẨN ĐẠI HỌC KINH TẾ HUẾ THEO ẢNH NGƯỜI DÙNG -->
    <div class=""cover-page"">
        <div class=""cover-univ"">ĐẠI HỌC HUẾ</div>
        <div class=""cover-school"">TRƯỜNG ĐẠI HỌC KINH TẾ</div>
        <div class=""cover-dept"">KHOA HỆ THỐNG THÔNG TIN KINH TẾ</div>
        
        <div class=""cover-logo-box"">
            {(string.IsNullOrEmpty(logoBase64) 
                ? "<div style='font-size: 36pt;'>🎓</div>" 
                : $"<img src='data:image/png;base64,{logoBase64}' class='cover-logo' alt='Logo HCE'/>")}
        </div>
        
        <div class=""cover-report-type"">BÁO CÁO THỰC TẬP NGHỀ NGHIỆP</div>
        <div class=""cover-title"">XÂY DỰNG HỆ THỐNG QUẢN LÝ KHO BÁN LẺ &amp; BÁN HÀNG TẠI QUẦY (POS)</div>
        
        <table class=""cover-meta-table"">
            <tr>
                <td class=""cover-meta-label"">Giảng viên hướng dẫn:</td>
                <td class=""cover-meta-value"">TS. Hà Ngọc Long</td>
            </tr>
            <tr>
                <td class=""cover-meta-label"">Sinh viên thực hiện:</td>
                <td class=""cover-meta-value"">Nguyễn Trung Quyết</td>
            </tr>
            <tr>
                <td class=""cover-meta-label"">Mã sinh viên:</td>
                <td class=""cover-meta-value"">23K4080044</td>
            </tr>
            <tr>
                <td class=""cover-meta-label"">Lớp:</td>
                <td class=""cover-meta-value"">K57 Tin học Kinh tế</td>
            </tr>
            <tr>
                <td class=""cover-meta-label"">Đơn vị thực tập:</td>
                <td class=""cover-meta-value"">Công ty Cổ phần Đầu tư Công nghệ TST ECO</td>
            </tr>
        </table>
        
        <div class=""cover-footer"">
            Huế, ngày 11/09/2026
        </div>
    </div>

    <!-- TOÀN BỘ NỘI DUNG TÀI LIỆU BÁO CÁO -->
    <div class=""report-body"">
        {bodyHtml}
    </div>

</body>
</html>";

            Console.WriteLine($">>> Đang lưu file HTML: {htmlPath} ...");
            File.WriteAllText(htmlPath, fullHtml, Encoding.UTF8);

            // Chuyển đổi sang Word DOCX và PDF thông qua Word COM Automation
            Console.WriteLine(">>> Đang khởi chạy Microsoft Word Automation để xuất DOCX và PDF ...");
            try
            {
                Type wordType = Type.GetTypeFromProgID("Word.Application");
                if (wordType == null)
                {
                    Console.WriteLine("Cảnh báo: Không tìm thấy Microsoft Word COM object.");
                    return;
                }

                dynamic word = Activator.CreateInstance(wordType);
                word.Visible = false;
                word.DisplayAlerts = 0;

                try
                {
                    Console.WriteLine(">>> Đang mở file HTML trong Word ...");
                    dynamic doc = word.Documents.Open(htmlPath, false, true);

                    // Thiết lập chân trang: Đánh số trang Page X / Y
                    try
                    {
                        foreach (dynamic section in doc.Sections)
                        {
                            dynamic footer = section.Footers(1); // 1 = wdHeaderFooterPrimary
                            footer.PageNumbers.Add(2, true); // 2 = wdAlignPageNumberRight
                        }
                    }
                    catch { }

                    Console.WriteLine($">>> Đang lưu file Word (.docx): {docxPath} ...");
                    // 16 = wdFormatXMLDocument (.docx)
                    doc.SaveAs2(docxPath, 16);
                    Console.WriteLine("===> ĐÃ TẠO THÀNH CÔNG FILE DOCX!");

                    Console.WriteLine($">>> Đang xuất file PDF (.pdf): {pdfPath} ...");
                    // 17 = wdFormatPDF (.pdf)
                    doc.SaveAs2(pdfPath, 17);
                    Console.WriteLine("===> ĐÃ TẠO THÀNH CÔNG FILE PDF!");

                    // Đếm số trang thực tế
                    int pageCount = doc.ComputeStatistics(2); // 2 = wdStatisticPages
                    Console.WriteLine($"\n===> SỐ TRANG THỰC TẾ TRONG WORD: {pageCount} TRANG");

                    doc.Close(false);
                }
                finally
                {
                    word.Quit(false);
                }

                Console.WriteLine("\n==========================================================");
                Console.WriteLine("HOÀN TẤT THÀNH CÔNG 100%!");
                Console.WriteLine($"1. File Word (.docx): {docxPath}");
                Console.WriteLine($"2. File PDF  (.pdf) : {pdfPath}");
                Console.WriteLine("==========================================================");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi Word COM: {ex.Message}");
            }
        }
    }
}
