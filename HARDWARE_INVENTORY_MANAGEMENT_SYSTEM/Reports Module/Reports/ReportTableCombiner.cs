using System;
using System.Collections.Generic;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module
{
    public static class ReportTableCombiner
    {
        public static ReportTable BuildModuleReport(string title, string subtitle, IList<ReportTable> reports)
        {
            if (reports == null || reports.Count == 0)
            {
                return null;
            }

            int maxColumns = CalculateMaxColumns(reports);
            if (maxColumns == 0)
            {
                return null;
            }

            var combined = new ReportTable
            {
                Title = title,
                Subtitle = subtitle
            };

            combined.Headers.Add("Section");
            for (int i = 1; i <= maxColumns; i++)
            {
                combined.Headers.Add("Column " + i);
            }

            for (int i = 0; i < reports.Count; i++)
            {
                var report = reports[i];
                if (report == null)
                {
                    continue;
                }

                string sectionTitle = string.IsNullOrEmpty(report.Title) ? "Section " + (i + 1) : report.Title;
                combined.Rows.Add(new List<string> { sectionTitle });

                var headerRow = report.Headers ?? new List<string>();
                combined.Rows.Add(BuildRow("Headers", headerRow, maxColumns));

                if (report.Rows != null)
                {
                    for (int r = 0; r < report.Rows.Count; r++)
                    {
                        var row = report.Rows[r] ?? new List<string>();
                        combined.Rows.Add(BuildRow(string.Empty, row, maxColumns));
                    }
                }

                combined.Rows.Add(new List<string>());
            }

            return combined;
        }

        private static List<string> BuildRow(string sectionValue, IList<string> values, int maxColumns)
        {
            var row = new List<string>();
            row.Add(sectionValue);

            for (int i = 0; i < maxColumns; i++)
            {
                if (values != null && i < values.Count)
                {
                    row.Add(values[i]);
                }
                else
                {
                    row.Add(string.Empty);
                }
            }

            return row;
        }

        private static int CalculateMaxColumns(IList<ReportTable> reports)
        {
            int maxColumns = 0;
            for (int i = 0; i < reports.Count; i++)
            {
                var report = reports[i];
                if (report == null)
                {
                    continue;
                }

                if (report.Headers != null && report.Headers.Count > maxColumns)
                {
                    maxColumns = report.Headers.Count;
                }

                if (report.Rows != null)
                {
                    for (int r = 0; r < report.Rows.Count; r++)
                    {
                        var row = report.Rows[r];
                        if (row != null && row.Count > maxColumns)
                        {
                            maxColumns = row.Count;
                        }
                    }
                }
            }

            return maxColumns;
        }
    }
}
