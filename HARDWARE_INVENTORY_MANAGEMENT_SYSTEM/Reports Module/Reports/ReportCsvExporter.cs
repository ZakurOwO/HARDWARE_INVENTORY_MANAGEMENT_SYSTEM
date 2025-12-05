using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module
{
    public static class ReportCsvExporter
    {
        public static bool ExportReportTableToCsv(ReportTable report)
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

            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
                sfd.Title = "Save Report as CSV";
                sfd.FileName = (string.IsNullOrEmpty(report.Title) ? "Report" : report.Title) + ".csv";

                if (sfd.ShowDialog() != DialogResult.OK)
                {
                    return false;
                }

                try
                {
                    WriteReportToCsv(report, sfd.FileName);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Failed to export report: " + ex.Message,
                        "Export Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        public static bool ExportDataTableToCsv(DataTable table, string title, string subtitle)
        {
            ReportTable report = ReportTableFactory.FromDataTable(table, title, subtitle);
            return ExportReportTableToCsv(report);
        }

        public static bool ExportDataGridViewToCsv(DataGridView grid, string title, string subtitle)
        {
            if (grid == null)
            {
                MessageBox.Show("No data grid to export", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            var report = new ReportTable
            {
                Title = title,
                Subtitle = subtitle,
                Headers = new List<string>(),
                Rows = new List<List<string>>()
            };

            foreach (DataGridViewColumn column in grid.Columns)
            {
                report.Headers.Add(column.HeaderText);
            }

            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.IsNewRow) continue;

                var rowValues = new List<string>();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    rowValues.Add(cell.Value == null ? string.Empty : cell.Value.ToString());
                }
                report.Rows.Add(rowValues);
            }

            return ExportReportTableToCsv(report);
        }

        private static bool ValidateReport(ReportTable report, out string errorMessage)
        {
            if (report == null)
            {
                errorMessage = "No data to export";
                return false;
            }

            if (report.Headers == null || report.Headers.Count == 0)
            {
                errorMessage = "No data to export";
                return false;
            }

            if (report.Rows == null || report.Rows.Count == 0)
            {
                errorMessage = "No data to export";
                return false;
            }

            errorMessage = null;
            return true;
        }

        private static void WriteReportToCsv(ReportTable report, string filePath)
        {
            var encoding = new UTF8Encoding(true);
            using (var writer = new StreamWriter(filePath, false, encoding))
            {
                string subtitle = BuildSubtitle(report.Subtitle);
                writer.WriteLine(report.Title ?? "Report");
                writer.WriteLine(subtitle);
                writer.WriteLine();

                List<List<string>> normalizedRows = NormalizeRows(report.Rows, report.Headers.Count);

                WriteRow(writer, report.Headers);
                foreach (var row in normalizedRows)
                {
                    WriteRow(writer, row);
                }
            }
        }

        private static List<List<string>> NormalizeRows(List<List<string>> rows, int expectedColumns)
        {
            var normalized = new List<List<string>>();

            foreach (var row in rows)
            {
                var safeRow = row ?? new List<string>();
                var newRow = new List<string>(expectedColumns);

                for (int i = 0; i < expectedColumns; i++)
                {
                    if (safeRow.Count > i)
                    {
                        newRow.Add(safeRow[i] ?? string.Empty);
                    }
                    else
                    {
                        newRow.Add(string.Empty);
                    }
                }

                normalized.Add(newRow);
            }

            return normalized;
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

        private static void WriteRow(StreamWriter writer, List<string> values)
        {
            for (int i = 0; i < values.Count; i++)
            {
                if (i > 0)
                {
                    writer.Write(',');
                }

                string value = values[i] ?? string.Empty;
                bool requiresQuotes = value.IndexOfAny(new[] { ',', '"', '\n', '\r' }) >= 0;

                if (requiresQuotes)
                {
                    value = value.Replace("\"", "\"\"");
                    writer.Write('"');
                    writer.Write(value);
                    writer.Write('"');
                }
                else
                {
                    writer.Write(value);
                }
            }

            writer.WriteLine();
        }
    }
}
