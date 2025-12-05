using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module
{
    public static class ReportPdfExporter
    {
        public static bool ExportReportTable(ReportTable report)
        {
            if (report == null)
            {
                MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (report.Headers == null || report.Headers.Count == 0)
            {
                MessageBox.Show("Report has no headers to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (report.Rows == null || report.Rows.Count == 0)
            {
                MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            string filePath;
            if (!TryGetSavePath(report.Title, out filePath))
            {
                return false;
            }

            try
            {
                PdfFont regularFont = GetRegularFont();
                PdfFont boldFont = GetBoldFont();
                int columnCount = Math.Max(report.Headers.Count, 1);
                List<List<string>> normalizedRows = NormalizeRows(report.Rows, columnCount);

                using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                using (PdfWriter pdfWriter = new PdfWriter(stream))
                using (PdfDocument pdfDocument = new PdfDocument(pdfWriter))
                {
                    pdfDocument.SetDefaultPageSize(PageSize.A4.Rotate());

                    using (Document document = new Document(pdfDocument))
                    {
                        document.SetMargins(40, 36, 40, 36);
                        AddDocumentHeader(document, report, regularFont, boldFont);
                        Table table = BuildTable(report.Headers, normalizedRows, regularFont, boldFont, columnCount);
                        document.Add(table);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Failed to export report: " + ex.GetType().FullName + ": " + ex.Message + "\n" + ex.StackTrace,
                    "Export Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }
        }

        private static PdfFont GetRegularFont()
        {
            return PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
        }

        private static PdfFont GetBoldFont()
        {
            return PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
        }

        private static void AddDocumentHeader(Document document, ReportTable report, PdfFont regularFont, PdfFont boldFont)
        {
            Paragraph title = new Paragraph(report.Title ?? "Report")
                .SetFont(boldFont)
                .SetFontSize(16)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetMarginBottom(6f);
            document.Add(title);

            string subtitleText = report.Subtitle;
            if (string.IsNullOrEmpty(subtitleText))
            {
                subtitleText = "Generated: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            }
            else
            {
                subtitleText = subtitleText + "\nGenerated: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            }

            Paragraph subtitle = new Paragraph(subtitleText)
                .SetFont(regularFont)
                .SetFontSize(10)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetMarginBottom(12f);
            document.Add(subtitle);
        }

        private static Table BuildTable(List<string> headers, List<List<string>> rows, PdfFont regularFont, PdfFont boldFont, int columnCount)
        {
            Table table = new Table(columnCount)
                .SetWidth(UnitValue.CreatePercentValue(100))
                .SetMarginTop(10f);

            if (headers != null)
            {
                foreach (string header in headers)
                {
                    Cell cell = new Cell()
                        .Add(new Paragraph(header ?? string.Empty).SetFont(boldFont).SetFontSize(10))
                        .SetBackgroundColor(new DeviceRgb(230, 230, 230))
                        .SetPadding(5)
                        .SetTextAlignment(TextAlignment.CENTER);
                    table.AddCell(cell);
                }
            }

            if (rows != null)
            {
                for (int i = 0; i < rows.Count; i++)
                {
                    List<string> row = rows[i];
                    for (int j = 0; j < columnCount; j++)
                    {
                        string value = row[j] ?? string.Empty;
                        Cell cell = new Cell()
                            .Add(new Paragraph(value).SetFont(regularFont).SetFontSize(10))
                            .SetPadding(5);
                        table.AddCell(cell);
                    }
                }
            }

            return table;
        }

        private static List<List<string>> NormalizeRows(List<List<string>> rows, int headerCount)
        {
            List<List<string>> normalized = new List<List<string>>();

            if (rows == null)
            {
                return normalized;
            }

            for (int i = 0; i < rows.Count; i++)
            {
                List<string> currentRow = rows[i] ?? new List<string>();
                List<string> normalizedRow = new List<string>(headerCount);

                for (int j = 0; j < headerCount; j++)
                {
                    if (j < currentRow.Count)
                    {
                        normalizedRow.Add(currentRow[j] ?? string.Empty);
                    }
                    else
                    {
                        normalizedRow.Add(string.Empty);
                    }
                }

                normalized.Add(normalizedRow);
            }

            return normalized;
        }

        private static bool TryGetSavePath(string reportTitle, out string path)
        {
            path = null;
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "PDF Files (*.pdf)|*.pdf";
                saveDialog.Title = "Save Report as PDF";
                saveDialog.FileName = SanitizeFileName((reportTitle ?? "report") + "_" + DateTime.Now.ToString("yyyyMMdd")) + ".pdf";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    path = saveDialog.FileName;
                    return true;
                }
            }

            return false;
        }

        private static string SanitizeFileName(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return "report";
            }

            for (int i = 0; i < System.IO.Path.GetInvalidFileNameChars().Length; i++)
            {
                char c = System.IO.Path.GetInvalidFileNameChars()[i];
                input = input.Replace(c, '_');
            }

            return input;
        }
    }
}
