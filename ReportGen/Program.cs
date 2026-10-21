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
            string rootDir = Directory.GetCurrentDirectory();
            string mdPath = Path.Combine(rootDir, "baocaodemo.md");
            string htmlPath = Path.Combine(rootDir, "baocaodemo.html");
            string docxPath = Path.Combine(rootDir, "baocaodemo.docx");
            string pdfPath = Path.Combine(rootDir, "baocaodemo.pdf");

            Console.WriteLine($">>> Đang đọc file: {mdPath} ...");
            if (!File.Exists(mdPath))
            {
                Console.WriteLine("Lỗi: Không tìm thấy file baocaodemo.md!");
                return;
            }

            string mdContent = File.ReadAllText(mdPath, Encoding.UTF8);

            // Cấu hình Markdig với Advanced Extensions (Tables, TaskLists, Footnotes, etc.)
            var pipeline = new MarkdownPipelineBuilder()
                .UseAdvancedExtensions()
                .Build();

            Console.WriteLine(">>> Đang chuyển đổi Markdown sang HTML chuẩn in ấn ...");
            string bodyHtml = Markdown.ToHtml(mdContent, pipeline);

            // Tùy biến thêm các thẻ để trình bày chuẩn Word / PDF
            // Thêm ngắt trang trước mỗi CHƯƠNG
            bodyHtml = Regex.Replace(bodyHtml, @"<h2 id=""chương-", @"<div style=""page-break-before: always;""></div><h2 id=""chương-");
            bodyHtml = Regex.Replace(bodyHtml, @"<h2>CHƯƠNG ", @"<div style=""page-break-before: always;""></div><h2>CHƯƠNG ");

            // Bọc bảng trong div và thêm class
            bodyHtml = bodyHtml.Replace("<table>", "<table class=\"doc-table\" border=\"1\" cellspacing=\"0\" cellpadding=\"6\">");

            // Xây dựng tài liệu hoàn chỉnh với CSS in ấn A4 chuẩn Office Word
            string fullHtml = $@"<!DOCTYPE html>
<html xmlns:o='urn:schemas-microsoft-com:office:office' 
      xmlns:w='urn:schemas-microsoft-com:office:word' 
      xmlns='http://www.w3.org/TR/REC-html40'>
<head>
    <meta charset=""utf-8"">
    <title>Báo Cáo Đồ Án Capstone - Kho Bán Lẻ</title>
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
            margin: 20mm 20mm 20mm 25mm; /* Chuẩn đồ án: Lề trái 25mm để đóng gáy, lề trên/dưới/phải 20mm */
            mso-header-margin: 10mm;
            mso-footer-margin: 10mm;
        }}

        body {{
            font-family: 'Times New Roman', Times, serif;
            font-size: 13pt;
            line-height: 1.45;
            color: #1a202c;
            background-color: #ffffff;
            margin: 0;
            padding: 0;
            text-align: justify;
        }}

        /* Trang bìa */
        .cover-page {{
            text-align: center;
            padding: 35px 20px;
            border: 3px double #2b6cb0;
            margin-bottom: 30px;
            page-break-after: always;
        }}
        .cover-inst {{
            font-size: 13pt;
            font-weight: bold;
            color: #2d3748;
            margin-bottom: 5px;
            text-transform: uppercase;
        }}
        .cover-faculty {{
            font-size: 12pt;
            font-weight: bold;
            color: #4a5568;
            margin-bottom: 40px;
        }}
        .cover-title {{
            font-size: 22pt;
            font-weight: bold;
            color: #1a365d;
            margin: 30px 0 15px 0;
            text-transform: uppercase;
            line-height: 1.3;
        }}
        .cover-subtitle {{
            font-size: 14pt;
            font-weight: bold;
            color: #2b6cb0;
            margin-bottom: 35px;
            line-height: 1.4;
        }}
        .cover-meta {{
            font-size: 12pt;
            text-align: left;
            margin: 40px auto;
            width: 85%;
            border-top: 1px solid #cbd5e1;
            padding-top: 20px;
            line-height: 1.8;
        }}
        .cover-footer {{
            font-size: 12pt;
            color: #718096;
            margin-top: 50px;
        }}

        /* Tiêu đề */
        h1 {{
            font-size: 17pt;
            font-weight: bold;
            color: #1a365d;
            margin-top: 25px;
            margin-bottom: 12px;
            text-align: center;
            text-transform: uppercase;
            page-break-after: avoid;
        }}

        h2 {{
            font-size: 15pt;
            font-weight: bold;
            color: #1a365d;
            margin-top: 28px;
            margin-bottom: 12px;
            padding-bottom: 5px;
            border-bottom: 2px solid #2b6cb0;
            text-transform: uppercase;
            page-break-after: avoid;
        }}

        h3 {{
            font-size: 13.5pt;
            font-weight: bold;
            color: #2b6cb0;
            margin-top: 20px;
            margin-bottom: 8px;
            page-break-after: avoid;
        }}

        h4 {{
            font-size: 13pt;
            font-weight: bold;
            font-style: italic;
            color: #2d3748;
            margin-top: 14px;
            margin-bottom: 6px;
            page-break-after: avoid;
        }}

        p {{
            margin: 0 0 10px 0;
            text-indent: 1.25cm;
        }}

        /* Bảng biểu */
        table.doc-table {{
            width: 100%;
            border-collapse: collapse;
            margin: 16px 0;
            font-size: 11pt;
            border: 1px solid #cbd5e1;
            page-break-inside: avoid;
        }}

        table.doc-table th {{
            background-color: #2b6cb0;
            color: #ffffff;
            font-weight: bold;
            text-align: center;
            padding: 8px 10px;
            border: 1px solid #2b6cb0;
        }}

        table.doc-table td {{
            padding: 7px 10px;
            border: 1px solid #cbd5e1;
            vertical-align: middle;
        }}

        table.doc-table tr:nth-child(even) {{
            background-color: #f8fafc;
        }}

        /* Khối mã nguồn */
        pre {{
            background-color: #f8fafc;
            border: 1px solid #e2e8f0;
            border-left: 4px solid #3b82f6;
            border-radius: 4px;
            padding: 10px 14px;
            margin: 14px 0;
            font-family: 'Consolas', 'Courier New', monospace;
            font-size: 10pt;
            line-height: 1.35;
            white-space: pre-wrap;
            word-break: break-all;
            page-break-inside: avoid;
        }}

        code {{
            font-family: 'Consolas', 'Courier New', monospace;
            font-size: 11pt;
            background-color: #edf2f7;
            color: #c53030;
            padding: 1px 4px;
            border-radius: 3px;
        }}

        pre code {{
            background-color: transparent;
            color: #1a202c;
            padding: 0;
            border: none;
            font-size: 10pt;
        }}

        /* Trích dẫn / Alert */
        blockquote {{
            margin: 14px 0;
            padding: 10px 16px;
            background-color: #ebf8ff;
            border-left: 4px solid #3182ce;
            border-radius: 3px;
            font-size: 11.5pt;
            page-break-inside: avoid;
        }}

        blockquote p {{
            margin: 0;
            text-indent: 0;
        }}

        /* Danh sách */
        ul, ol {{
            margin: 8px 0 12px 25px;
            padding: 0;
        }}

        li {{
            margin-bottom: 5px;
            line-height: 1.4;
        }}

        hr {{
            border: 0;
            height: 1px;
            background-color: #cbd5e1;
            margin: 24px 0;
        }}

        a {{
            color: #2b6cb0;
            text-decoration: underline;
        }}
    </style>
</head>
<body>

    <!-- TRANG BÌA ĐỒ ÁN CHUYÊN NGHIỆP -->
    <div class=""cover-page"">
        <div class=""cover-inst"">BỘ GIÁO DỤC VÀ ĐÀO TẠO</div>
        <div class=""cover-faculty"">HỌC PHẦN ĐỒ ÁN CAPSTONE (ECO2415) — BỘ MÔN THƯƠNG MẠI ĐIỆN TỬ</div>
        
        <div style=""height: 35px;""></div>
        
        <div class=""cover-title"">BÁO CÁO TỔNG KẾT ĐỒ ÁN CAPSTONE</div>
        <div class=""cover-subtitle"">ĐỀ TÀI 13: HỆ THỐNG QUẢN LÝ KHO BÁN LẺ & BÁN HÀNG TẠI QUẦY (POS)<br/>KIẾN TRÚC CLEAN ARCHITECTURE 4 TẦNG & MICROSOFT SQL SERVER</div>
        
        <div style=""height: 25px;""></div>
        
        <div class=""cover-meta"">
            <table style=""width: 100%; border: none; font-size: 12pt;"">
                <tr style=""border: none;""><td style=""width: 35%; border: none;""><b>Học phần:</b></td><td style=""border: none;"">Đồ án Capstone Thương Mại Điện Tử (ECO2415)</td></tr>
                <tr style=""border: none;""><td style=""border: none;""><b>Mã đề tài:</b></td><td style=""border: none;"">Đề tài số 13 (Kho bán lẻ: Nhập – Xuất – Tồn)</td></tr>
                <tr style=""border: none;""><td style=""border: none;""><b>Kiến trúc:</b></td><td style=""border: none;"">Clean Architecture 4 Tầng (.NET 8 Blazor Server)</td></tr>
                <tr style=""border: none;""><td style=""border: none;""><b>Cơ sở dữ liệu:</b></td><td style=""border: none;"">Microsoft SQL Server (Chuẩn 3NF, Dapper & 1 Transaction)</td></tr>
                <tr style=""border: none;""><td style=""border: none;""><b>Điểm tự đánh giá:</b></td><td style=""border: none;""><b>100 / 100 Điểm (Đạt đủ 17 mã K1.1 – K5.3)</b></td></tr>
            </table>
        </div>
        
        <div class=""cover-footer"">
            HỌC KỲ CAPSTONE 2026
        </div>
    </div>

    <!-- NỘI DUNG BÁO CÁO -->
    <div class=""main-content"">
        {bodyHtml}
    </div>

</body>
</html>";

            Console.WriteLine($">>> Đang lưu file HTML: {htmlPath} ...");
            File.WriteAllText(htmlPath, fullHtml, Encoding.UTF8);

            // Chuyển đổi sang Word (.docx) và PDF (.pdf) qua Word COM Automation
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
                    Console.WriteLine(">>> Đang mở tài liệu trong Word ...");
                    dynamic doc = word.Documents.Open(htmlPath, false, true);

                    Console.WriteLine($">>> Đang xuất file Word (.docx): {docxPath} ...");
                    // 16 = wdFormatXMLDocument (.docx)
                    doc.SaveAs2(docxPath, 16);
                    Console.WriteLine("===> Tạo thành công file DOCX!");

                    Console.WriteLine($">>> Đang xuất file PDF (.pdf): {pdfPath} ...");
                    // 17 = wdFormatPDF (.pdf)
                    doc.SaveAs2(pdfPath, 17);
                    Console.WriteLine("===> Tạo thành công file PDF!");

                    doc.Close(false);
                }
                finally
                {
                    word.Quit(false);
                }

                Console.WriteLine("\n==========================================================");
                Console.WriteLine("HOÀN TẤT THÀNH CÔNG 100%!");
                Console.WriteLine($"1. File Word: {docxPath}");
                Console.WriteLine($"2. File PDF : {pdfPath}");
                Console.WriteLine("==========================================================");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi Word COM: {ex.Message}");
            }
        }
    }
}
