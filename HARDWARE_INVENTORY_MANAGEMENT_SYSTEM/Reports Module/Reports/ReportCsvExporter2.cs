using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module
{
    public static class ReportCsvExporter2
    {
        public static bool ExportReportTable(ReportTable report)
        {
            string validationError;
            if (!ValidateReport(report, out validationError))
            {
                if (!string.IsNullOrEmpty(validationError))
                {
                    MessageBox.Show(validationError, "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return false;
            }

            string safeTitle = string.IsNullOrWhiteSpace(report.Title) ? "Report" : report.Title.Trim();
            string defaultFileName = SanitizeFileName(safeTitle) + ".csv";

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
            dialog.Title = "Save Report as CSV";
            dialog.FileName = defaultFileName;

            DialogResult result = dialog.ShowDialog();
            if (result != DialogResult.OK)
            {
                return false;
            }

            try
            {
                WriteReportToFile(report, dialog.FileName);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to export report: " + ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool ExportDataTable(DataTable table, string title, string subtitle)
        {
            ReportTable report = ReportTableFactory.FromDataTable(table, title, subtitle);
            return ExportReportTable(report);
        }

        public static bool ExportDataGridView(DataGridView grid, string title, string subtitle)
        {
            if (grid == null)
            {
                MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            ReportTable report = new ReportTable();
            report.Title = title;
            report.Subtitle = subtitle;

            foreach (DataGridViewColumn column in grid.Columns)
            {
                report.Headers.Add(column.HeaderText);
            }

            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }

                List<string> values = new List<string>();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    values.Add(cell.Value == null ? string.Empty : cell.Value.ToString());
                }

                report.Rows.Add(values);
            }

            return ExportReportTable(report);
        }

        public static bool ExportModule(string moduleName, IList<ReportTable> reports)
        {
            if (reports == null || reports.Count == 0)
            {
                MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();
            if (result != DialogResult.OK || string.IsNullOrEmpty(dialog.SelectedPath))
            {
                return false;
            }

            string safeModuleName = string.IsNullOrWhiteSpace(moduleName) ? "Module" : SanitizeFileName(moduleName);
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            int exportedCount = 0;

            foreach (ReportTable report in reports)
            {
                string validationError;
                if (!ValidateReport(report, out validationError))
                {
                    continue;
                }

                string safeTitle = string.IsNullOrWhiteSpace(report.Title) ? "Report" : report.Title.Trim();
                string sanitizedTitle = SanitizeFileName(safeTitle);
                string fileName = safeModuleName + "_" + sanitizedTitle + "_" + timestamp + ".csv";
                string filePath = Path.Combine(dialog.SelectedPath, fileName);

                try
                {
                    WriteReportToFile(report, filePath);
                    exportedCount++;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to export " + safeTitle + ": " + ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (exportedCount == 0)
            {
                MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            MessageBox.Show("Exported " + exportedCount + " report(s) to CSV.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }

        private static void WriteReportToFile(ReportTable report, string filePath)
        {
            List<string> headers = PrepareHeaders(report);
            if (headers == null || headers.Count == 0)
            {
                throw new InvalidOperationException("No headers available for export.");
            }

            int columnCount = headers.Count;
            List<List<string>> normalizedRows = NormalizeRows(report.Rows, columnCount);

            UTF8Encoding encoding = new UTF8Encoding(true);
            using (StreamWriter writer = new StreamWriter(filePath, false, encoding))
            {
                string titleLine = string.IsNullOrWhiteSpace(report.Title) ? "Report" : report.Title;
                string subtitleLine = BuildSubtitle(report.Subtitle);

                writer.WriteLine(titleLine);
                writer.WriteLine(subtitleLine);
                writer.WriteLine();

                WriteRow(writer, headers);
                foreach (List<string> row in normalizedRows)
                {
                    WriteRow(writer, row);
                }
            }
        }

        private static List<string> PrepareHeaders(ReportTable report)
        {
            if (report != null && report.Headers != null && report.Headers.Count > 0)
            {
                List<string> sanitizedHeaders = new List<string>();
                for (int i = 0; i < report.Headers.Count; i++)
                {
                    sanitizedHeaders.Add(report.Headers[i] ?? string.Empty);
                }
                return sanitizedHeaders;
            }

            int maxColumns = 0;
            if (report != null && report.Rows != null)
            {
                foreach (List<string> row in report.Rows)
                {
                    if (row != null && row.Count > maxColumns)
                    {
                        maxColumns = row.Count;
                    }
                }
            }

            List<string> defaultHeaders = new List<string>();
            for (int i = 0; i < maxColumns; i++)
            {
                defaultHeaders.Add("Column " + (i + 1));
            }

            if (defaultHeaders.Count == 0 && report != null && report.Rows != null && report.Rows.Count > 0)
            {
                defaultHeaders.Add("Column 1");
            }

            return defaultHeaders;
        }

        private static List<List<string>> NormalizeRows(List<List<string>> rows, int columnCount)
        {
            List<List<string>> normalized = new List<List<string>>();

            if (rows == null)
            {
                return normalized;
            }

            foreach (List<string> row in rows)
            {
                List<string> safeRow = row == null ? new List<string>() : new List<string>(row);
                List<string> paddedRow = new List<string>();

                for (int i = 0; i < columnCount; i++)
                {
                    if (i < safeRow.Count)
                    {
                        paddedRow.Add(safeRow[i] ?? string.Empty);
                    }
                    else
                    {
                        paddedRow.Add(string.Empty);
                    }
                }

                if (paddedRow.Count > columnCount)
                {
                    paddedRow = paddedRow.GetRange(0, columnCount);
                }

                normalized.Add(paddedRow);
            }

            return normalized;
        }

        private static void WriteRow(StreamWriter writer, IList<string> values)
        {
            for (int i = 0; i < values.Count; i++)
            {
                if (i > 0)
                {
                    writer.Write(',');
                }

                string value = values[i] ?? string.Empty;
                bool requiresQuotes = value.IndexOf(',') >= 0 || value.IndexOf('"') >= 0 || value.IndexOf('\n') >= 0 || value.IndexOf('\r') >= 0;

                if (requiresQuotes)
                {
                    string escaped = value.Replace("\"", "\"\"");
                    writer.Write('"');
                    writer.Write(escaped);
                    writer.Write('"');
                }
                else
                {
                    writer.Write(value);
                }
            }

            writer.WriteLine();
        }

        private static bool ValidateReport(ReportTable report, out string errorMessage)
        {
            if (report == null)
            {
                errorMessage = "No data to export.";
                return false;
            }

            bool hasRows = report.Rows != null && report.Rows.Count > 0;
            if (!hasRows)
            {
                errorMessage = "No data to export.";
                return false;
            }

            errorMessage = null;
            return true;
        }

        private static string BuildSubtitle(string subtitle)
        {
            string timestamp = "Generated: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            if (string.IsNullOrEmpty(subtitle))
            {
                return timestamp;
            }

            return subtitle + " | " + timestamp;
        }

        private static string SanitizeFileName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return "Report";
            }

            char[] invalidChars = Path.GetInvalidFileNameChars();
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < name.Length; i++)
            {
                char c = name[i];
                bool isInvalid = false;
                for (int j = 0; j < invalidChars.Length; j++)
                {
                    if (c == invalidChars[j])
                    {
                        isInvalid = true;
                        break;
                    }
                }

                if (isInvalid)
                {
                    builder.Append('_');
                }
                else
                {
                    builder.Append(c);
                }
            }

            string sanitized = builder.ToString().Trim();
            if (sanitized.Length == 0)
            {
                return "Report";
            }

            return sanitized;
        }
    }
}
