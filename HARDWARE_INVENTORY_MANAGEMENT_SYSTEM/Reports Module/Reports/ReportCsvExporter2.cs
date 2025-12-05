using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module
{
    public static class ReportCsvExporter2
    {
        // ------------------------------------------------------
        // EXPORT A SINGLE REPORT
        // ------------------------------------------------------
        public static bool ExportReportTable(ReportTable report)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV File (*.csv)|*.csv";
                sfd.Title = "Export Report";
                sfd.FileName = report.Title + ".csv";

                DialogResult result = sfd.ShowDialog();

                // ❌ DO NOT EXPORT if user presses Cancel
                if (result != DialogResult.OK || string.IsNullOrWhiteSpace(sfd.FileName))
                    return false;

                try
                {
                    using (StreamWriter writer = new StreamWriter(sfd.FileName))
                    {
                        // Headers
                        writer.WriteLine(string.Join(",", report.Headers));

                        // Rows
                        foreach (var row in report.Rows)
                        {
                            writer.WriteLine(string.Join(",", row));
                        }
                    }

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        // ------------------------------------------------------
        // EXPORT AN ENTIRE MODULE (MULTIPLE REPORT PAGES)
        // ------------------------------------------------------
        public static bool ExportModule(string moduleName, List<ReportTable> reports)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV File (*.csv)|*.csv";
                sfd.Title = "Export Full Module";
                sfd.FileName = moduleName + " Module Export.csv";

                DialogResult result = sfd.ShowDialog();

                // ❌ DO NOT EXPORT if user cancels
                if (result != DialogResult.OK || string.IsNullOrWhiteSpace(sfd.FileName))
                    return false;

                try
                {
                    using (StreamWriter writer = new StreamWriter(sfd.FileName))
                    {
                        foreach (ReportTable report in reports)
                        {
                            writer.WriteLine("===== " + report.Title + " =====");
                            writer.WriteLine(string.Join(",", report.Headers));

                            foreach (var row in report.Rows)
                                writer.WriteLine(string.Join(",", row));

                            writer.WriteLine();
                        }
                    }

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
