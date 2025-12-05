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
    public static class ReportPdfExporter1
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
                MessageBox.Show("Unable to export: the report has no headers defined.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (report.Rows == null || report.Rows.Count == 0)
            {
                MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            ReportTable normalizedReport = NormalizeReport(report);

            string filePath;
            if (!TryGetSavePath(normalizedReport.Title, out filePath))
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
                        AddDocumentHeader(document, normalizedReport);
                        Table table = BuildTable(normalizedReport);
                        document.Add(table);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                string message = BuildFullExceptionMessage(ex);
                MessageBox.Show("Failed to export report: " + message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (columnCount <= 0)
            {
                throw new InvalidOperationException("Cannot build table with zero columns.");
            }

            List<List<string>> normalizedRows = report.Rows ?? new List<List<string>>();

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

                return table;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to build PDF table: " + ex.Message, ex);
            }
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

            if (report.Rows == null || report.Rows.Count == 0)
            {
                MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (string.IsNullOrWhiteSpace(filePath))
            {
                MessageBox.Show("Invalid file path.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string directory = Path.GetDirectoryName(filePath);
            string fileNameOnly = Path.GetFileNameWithoutExtension(filePath);
            string sanitizedName = SanitizeFileName(fileNameOnly);
            string ensuredExtension = Path.ChangeExtension(sanitizedName, ".pdf");
            if (string.IsNullOrEmpty(directory))
            {
                filePath = ensuredExtension;
            }
            else
            {
                filePath = Path.Combine(directory, ensuredExtension);
            }

            ReportTable normalizedReport = NormalizeReport(report);

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
                        AddDocumentHeader(document, normalizedReport);
                        Table table = BuildTable(normalizedReport);
                        document.Add(table);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                string message = BuildFullExceptionMessage(ex);
                MessageBox.Show("Failed to export report: " + message, "Export Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private static ReportTable NormalizeReport(ReportTable report)
        {
            ReportTable normalized = new ReportTable();
            normalized.Title = report.Title;
            normalized.Subtitle = report.Subtitle;

            normalized.Headers = new List<string>();
            if (report.Headers != null)
            {
                for (int i = 0; i < report.Headers.Count; i++)
                {
                    normalized.Headers.Add(report.Headers[i] ?? string.Empty);
                }
            }

            int columnCount = normalized.Headers.Count;
            normalized.Rows = new List<List<string>>();

            if (report.Rows != null)
            {
                for (int r = 0; r < report.Rows.Count; r++)
                {
                    List<string> sourceRow = report.Rows[r] ?? new List<string>();
                    List<string> normalizedRow = new List<string>();

                    for (int c = 0; c < columnCount; c++)
                    {
                        if (c < sourceRow.Count)
                        {
                            normalizedRow.Add(sourceRow[c] ?? string.Empty);
                        }
                        else
                        {
                            normalizedRow.Add(string.Empty);
                        }
                    }

                    normalized.Rows.Add(normalizedRow);
                }
            }

            return normalized;
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

            string working = input.Replace('\u00A0', ' ').Replace("₱", "PHP ");
            StringBuilder builder = new StringBuilder(working.Length);

            for (int i = 0; i < working.Length; i++)
            {
                char current = working[i];

                if (char.IsSurrogate(current))
                {
                    // Skip surrogate pairs/emojis
                    if (char.IsHighSurrogate(current) && i + 1 < working.Length && char.IsLowSurrogate(working[i + 1]))
                    {
                        i++;
                    }
                    continue;
                }

                if (current == '\r')
                {
                    continue;
                }

                if (current == '\n')
                {
                    builder.Append('\n');
                    continue;
                }

                if (char.IsWhiteSpace(current))
                {
                    builder.Append(' ');
                    continue;
                }

                if (char.IsControl(current))
                {
                    continue;
                }

                if (current < 32)
                {
                    continue;
                }

                if (current > '\u024F')
                {
                    continue;
                }

                builder.Append(current);
            }

            return builder.ToString();
        }

        private static PdfFont GetRegularFont()
        {
            try
            {
                return PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            }
            catch (Exception)
            {
                try
                {
                    return PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
                }
                catch (Exception)
                {
                    return PdfFontFactory.CreateFont(StandardFonts.COURIER);
                }
            }
        }

        private static PdfFont GetBoldFont()
        {
            try
            {
                return PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            }
            catch (Exception)
            {
                try
                {
                    return PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);
                }
                catch (Exception)
                {
                    return PdfFontFactory.CreateFont(StandardFonts.COURIER_BOLD);
                }
            }
        }

        private static string BuildFullExceptionMessage(Exception ex)
        {
            StringBuilder builder = new StringBuilder();
            AppendException(builder, ex, string.Empty);
            return builder.ToString();
        }

        private static void AppendException(StringBuilder builder, Exception ex, string prefix)
        {
            if (ex == null)
            {
                return;
            }

            if (builder.Length > 0)
            {
                builder.AppendLine();
                builder.AppendLine();
            }

            builder.Append(prefix);
            builder.Append(ex.GetType().FullName);
            builder.AppendLine();
            builder.Append(ex.Message);
            builder.AppendLine();
            builder.Append(ex.StackTrace);

            if (ex.InnerException != null)
            {
                AppendException(builder, ex.InnerException, "Inner Exception: ");
            }
        }
    }
}
