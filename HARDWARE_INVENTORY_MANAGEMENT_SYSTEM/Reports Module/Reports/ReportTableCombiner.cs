using System;
using System.Collections.Generic;
using System.Linq;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module
{
    /// <summary>
    /// Combines multiple ReportTable objects into a single ReportTable that can be exported.
    /// Handles different headers across reports by creating a "union header" (superset).
    /// </summary>
    public static class ReportTableCombiner
    {
        /// <summary>
        /// Builds a single combined report that contains rows from multiple reports.
        /// The combined report will have a first column called "Report" to identify the source.
        /// </summary>
        public static ReportTable BuildModuleReport(string moduleTitle, string moduleSubtitle, IEnumerable<ReportTable> reports)
        {
            if (reports == null) return null;

            var validReports = reports
                .Where(r => r != null && r.Headers != null && r.Headers.Count > 0 && r.Rows != null && r.Rows.Count > 0)
                .ToList();

            if (validReports.Count == 0) return null;

            // 1) Build union headers in stable order (first seen wins ordering)
            var unionHeaders = BuildUnionHeaders(validReports);

            // 2) Create combined report
            var combined = new ReportTable
            {
                Title = string.IsNullOrWhiteSpace(moduleTitle) ? "Module Reports" : moduleTitle,
                Subtitle = moduleSubtitle ?? string.Empty,
                Headers = unionHeaders,
                Rows = new List<List<string>>()
            };

            // 3) Append each report as a "section" inside the combined rows
            foreach (var report in validReports)
            {
                AppendReportSection(combined, report);
            }

            return combined;
        }

        private static List<string> BuildUnionHeaders(List<ReportTable> reports)
        {
            // Always include the first column for report/source name
            var union = new List<string> { "Report" };
            var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            // "Report" is already added
            seen.Add("Report");

            foreach (var r in reports)
            {
                foreach (var h in r.Headers)
                {
                    var header = (h ?? string.Empty).Trim();
                    if (header.Length == 0) continue;

                    if (seen.Add(header))
                        union.Add(header);
                }
            }

            // If a report had empty headers for some reason, still keep at least 1 header
            if (union.Count == 0) union.Add("Report");

            return union;
        }

        private static void AppendReportSection(ReportTable combined, ReportTable report)
        {
            int colCount = combined.Headers.Count;

            // Optional section separator row (blank)
            combined.Rows.Add(BlankRow(colCount));

            // Section title row
            combined.Rows.Add(SectionTitleRow(colCount, report.Title ?? "Report"));

            // Add the report's rows mapped into the union columns
            // Map header name -> index in the report
            var headerIndex = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < report.Headers.Count; i++)
            {
                var name = (report.Headers[i] ?? string.Empty).Trim();
                if (name.Length == 0) continue;

                // first occurrence wins
                if (!headerIndex.ContainsKey(name))
                    headerIndex[name] = i;
            }

            foreach (var srcRow in report.Rows)
            {
                var dst = BlankRow(colCount);

                // Column 0 is "Report"
                dst[0] = report.Title ?? "Report";

                // For each union header (except "Report"), fill from src if that header exists in the report
                for (int u = 1; u < combined.Headers.Count; u++)
                {
                    string unionHeader = combined.Headers[u];

                    if (headerIndex.TryGetValue(unionHeader, out int srcIndex))
                    {
                        if (srcRow != null && srcIndex >= 0 && srcIndex < srcRow.Count)
                            dst[u] = srcRow[srcIndex] ?? string.Empty;
                        else
                            dst[u] = string.Empty;
                    }
                    else
                    {
                        dst[u] = string.Empty;
                    }
                }

                combined.Rows.Add(dst);
            }
        }

        private static List<string> BlankRow(int colCount)
        {
            var row = new List<string>(colCount);
            for (int i = 0; i < colCount; i++) row.Add(string.Empty);
            return row;
        }

        private static List<string> SectionTitleRow(int colCount, string title)
        {
            var row = BlankRow(colCount);
            row[0] = "=== " + title + " ===";
            return row;
        }
    }
}
