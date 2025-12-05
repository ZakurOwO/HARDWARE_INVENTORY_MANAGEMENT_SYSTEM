using System;
using System.Collections.Generic;
using System.Data;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module
{
    public static class ReportTableFactory
    {
        public static ReportTable FromDataTable(DataTable dt, string title, string subtitle = null)
        {
            var report = new ReportTable
            {
                Title = title,
                Subtitle = subtitle ?? string.Empty
            };

            if (dt == null)
            {
                return report;
            }

            foreach (DataColumn col in dt.Columns)
            {
                report.Headers.Add(col.ColumnName);
            }

            foreach (DataRow dr in dt.Rows)
            {
                var row = new List<string>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(dr[col] == null || dr[col] == DBNull.Value ? string.Empty : dr[col].ToString());
                }

                report.Rows.Add(row);
            }

            return report;
        }
    }
}
