using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
        private static PdfFont _regularFont;
        private static PdfFont _boldFont;

        public static bool ExportReportTable(ReportTable report)
        {
            if (report == null)
            {
                MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (report.Headers == null || report.Headers.Count == 0)
            {
                MessageBox.Show("Unable to export: the report has no headers defined.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (report.Rows == null)
            {
                report.Rows = new List<List<string>>();
            }

            string filePath;
            if (!TryGetSavePath(report.Title, out filePath))
            {
                return false;
            }

            try
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                using (PdfWriter pdfWriter = new PdfWriter(stream))
                using (PdfDocument pdfDocument = new PdfDocument(pdfWriter))
                {
                    pdfDocument.SetDefaultPageSize(PageSize.A4.Rotate());

                    using (Document document = new Document(pdfDocument))
                    {
                        document.SetMargins(40, 36, 40, 36);
                        AddDocumentHeader(document, report);
                        Table table = BuildTable(report);
                        document.Add(table);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Exception root = ex.InnerException ?? ex;
                MessageBox.Show("Failed to export report: " + ex.Message + "\n" + root.GetType().FullName, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private static void AddDocumentHeader(Document document, ReportTable report)
        {
            Paragraph title = new Paragraph(SanitizeCellText(report.Title ?? "Report"))
                .SetFont(GetBoldFont())
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

            Paragraph subtitle = new Paragraph(SanitizeCellText(subtitleText))
                .SetFont(GetRegularFont())
                .SetFontSize(10)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetMarginBottom(12f);
            document.Add(subtitle);
        }

        private static Table BuildTable(ReportTable report)
        {
            if (report.Headers == null || report.Headers.Count == 0)
            {
                throw new InvalidOperationException("Cannot build table: no headers were provided.");
            }

            int columnCount = report.Headers.Count;
            List<List<string>> normalizedRows = NormalizeRows(report.Rows, columnCount);

            try
            {
                Table table = new Table(columnCount)
                    .SetWidth(UnitValue.CreatePercentValue(100))
                    .SetMarginTop(10f);

                for (int h = 0; h < report.Headers.Count; h++)
                {
                    string headerText = SanitizeCellText(report.Headers[h]);
                    Cell cell = new Cell()
                        .Add(new Paragraph(headerText).SetFont(GetBoldFont()).SetFontSize(10))
                        .SetBackgroundColor(new DeviceRgb(230, 230, 230))
                        .SetPadding(5)
                        .SetTextAlignment(TextAlignment.CENTER);
                    table.AddCell(cell);
                }

                if (normalizedRows != null)
                {
                    for (int i = 0; i < normalizedRows.Count; i++)
                    {
                        List<string> row = normalizedRows[i];
                        if (row == null)
                        {
                            continue;
                        }

                        for (int j = 0; j < columnCount; j++)
                        {
                            string value = SanitizeCellText(row[j]);
                            Cell cell = new Cell()
                                .Add(new Paragraph(value).SetFont(GetRegularFont()).SetFontSize(10))
                                .SetPadding(5);
                            table.AddCell(cell);
                        }
                    }
                }

                return table;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to build PDF table: " + ex.Message, ex);
            }
        }

        private static List<List<string>> NormalizeRows(List<List<string>> rows, int columnCount)
        {
            List<List<string>> normalized = new List<List<string>>();

            if (rows == null)
            {
                return normalized;
            }

            for (int i = 0; i < rows.Count; i++)
            {
                List<string> currentRow = rows[i] ?? new List<string>();
                List<string> normalizedRow = new List<string>(columnCount);

                for (int j = 0; j < columnCount; j++)
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

                if (currentRow.Count > columnCount)
                {
                    // Truncate extra cells beyond the expected column count
                    // (only the first 'columnCount' items were copied)
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
                    string selectedPath = saveDialog.FileName;
                    string directory = Path.GetDirectoryName(selectedPath);
                    string fileNameOnly = Path.GetFileNameWithoutExtension(selectedPath);
                    string sanitizedName = SanitizeFileName(fileNameOnly);
                    string ensuredExtension = Path.ChangeExtension(sanitizedName, ".pdf");
                    if (string.IsNullOrEmpty(directory))
                    {
                        path = ensuredExtension;
                    }
                    else
                    {
                        path = Path.Combine(directory, ensuredExtension);
                    }
                    return true;
                }
            }

            return false;
        }

        public static bool ExportReportTableToPath(ReportTable report, string filePath)
        {
            if (report == null)
            {
                MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (report.Headers == null || report.Headers.Count == 0)
            {
                MessageBox.Show("Unable to export: the report has no headers defined.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (report.Rows == null)
            {
                report.Rows = new List<List<string>>();
            }

            try
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                using (PdfWriter pdfWriter = new PdfWriter(stream))
                using (PdfDocument pdfDocument = new PdfDocument(pdfWriter))
                {
                    pdfDocument.SetDefaultPageSize(PageSize.A4.Rotate());

                    using (Document document = new Document(pdfDocument))
                    {
                        document.SetMargins(40, 36, 40, 36);
                        AddDocumentHeader(document, report);
                        Table table = BuildTable(report);
                        document.Add(table);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Exception root = ex.InnerException ?? ex;
                MessageBox.Show("Failed to export report: " + ex.Message + "\n" + root.GetType().FullName, "Export Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
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

        private static string SanitizeCellText(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            StringBuilder builder = new StringBuilder(input.Length);
            for (int i = 0; i < input.Length; i++)
            {
                char current = input[i];

                if (char.IsSurrogate(current))
                {
                    // Skip invalid standalone surrogate characters
                    continue;
                }

                if (current == '\r' || current == '\n')
                {
                    builder.Append(' ');
                    continue;
                }

                if (char.IsControl(current))
                {
                    continue;
                }

                builder.Append(current);
            }

            return builder.ToString();
        }

        private static PdfFont GetRegularFont()
        {
            if (_regularFont != null)
            {
                return _regularFont;
            }

            try
            {
                _regularFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            }
            catch (Exception)
            {
                _regularFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            }

            return _regularFont;
        }

        private static PdfFont GetBoldFont()
        {
            if (_boldFont != null)
            {
                return _boldFont;
            }

            try
            {
                _boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            }
            catch (Exception)
            {
                _boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            }

            return _boldFont;
        }
    }
}
