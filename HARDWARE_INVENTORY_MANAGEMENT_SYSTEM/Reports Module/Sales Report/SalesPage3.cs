using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Sales_Report
{
    public partial class SalesPage3 : UserControl, IReportExportable
    {
        private SalesReportDataAccess dataAccess;
        private DateTime? filterStartDate;
        private DateTime? filterEndDate;
        private bool showMonthlyView = false; // false = daily, true = monthly
        private List<SalesSummaryReport> currentData;

        public SalesPage3()
        {
            InitializeComponent();
            dataAccess = new SalesReportDataAccess();
            this.Load += SalesPage3_Load;
            ExportPDFBtn.Text = "Export CSV";
            ExportPDFBtn.Click += ExportPDFBtn_Click;
        }

        private void SalesPage3_Load(object sender, EventArgs e)
        {
            ConfigureDataGridView();
            LoadSalesData(filterStartDate, filterEndDate);
        }

        private void ConfigureDataGridView()
        {
            // Configure DataGridView appearance
            dgvCurrentStockReport.AutoGenerateColumns = false;
            dgvCurrentStockReport.AllowUserToAddRows = false;
            dgvCurrentStockReport.ReadOnly = true;
            dgvCurrentStockReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCurrentStockReport.MultiSelect = false;

            // Bind columns to data properties
            Date.DataPropertyName = "Date";
            NoOfTransactions.DataPropertyName = "NoOfTransactions";
            TotalQuantitySold.DataPropertyName = "TotalQuantitySold";
            TotalSales.DataPropertyName = "TotalSales";
            TotalProfit.DataPropertyName = "TotalProfit";
            AvgSalePerTransaction.DataPropertyName = "AvgSalePerTransaction";

            // Format date column
            Date.DefaultCellStyle.Format = showMonthlyView ? "MMM yyyy" : "yyyy-MM-dd";

            // Format currency columns
            TotalSales.DefaultCellStyle.Format = "₱#,##0.00";
            TotalProfit.DefaultCellStyle.Format = "₱#,##0.00";
            AvgSalePerTransaction.DefaultCellStyle.Format = "₱#,##0.00";

            // Align numeric columns
            NoOfTransactions.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            TotalQuantitySold.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            TotalSales.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            TotalProfit.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            AvgSalePerTransaction.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void LoadSalesData(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                // Show loading indicator
                this.Cursor = Cursors.WaitCursor;

                // Get sales data from database (daily or monthly)
                List<SalesSummaryReport> salesData;

                if (showMonthlyView)
                {
                    salesData = dataAccess.GetMonthlySalesSummary(startDate, endDate);
                    label2.Text = "Monthly Sales Summary";
                    Date.HeaderText = "Month";
                    Date.DefaultCellStyle.Format = "MMM yyyy";
                }
                else
                {
                    salesData = dataAccess.GetDailySalesSummary(startDate, endDate);
                    label2.Text = "Daily Sales Summary";
                    Date.HeaderText = "Date";
                    Date.DefaultCellStyle.Format = "yyyy-MM-dd";
                }

                currentData = salesData;

                // Bind to DataGridView
                dgvCurrentStockReport.DataSource = null;
                dgvCurrentStockReport.DataSource = salesData;

                // Calculate and display totals
                DisplaySummaryTotals(salesData);

                // Update status
                Console.WriteLine($"Loaded {salesData.Count} {(showMonthlyView ? "monthly" : "daily")} sales records");

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show($"Error loading sales summary: {ex.Message}",
                    "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplaySummaryTotals(List<SalesSummaryReport> data)
        {
            if (data == null || data.Count == 0) return;

            // Calculate totals
            int totalTransactions = data.Sum(x => x.NoOfTransactions);
            int totalQuantity = data.Sum(x => x.TotalQuantitySold);
            decimal totalSales = data.Sum(x => x.TotalSales);
            decimal totalProfit = data.Sum(x => x.TotalProfit);
            decimal avgPerTransaction = totalTransactions > 0 ? totalSales / totalTransactions : 0;

            // Display in console or add summary row
            Console.WriteLine($"=== SUMMARY ===");
            Console.WriteLine($"Total Transactions: {totalTransactions:N0}");
            Console.WriteLine($"Total Quantity: {totalQuantity:N0}");
            Console.WriteLine($"Total Sales: ₱{totalSales:N2}");
            Console.WriteLine($"Total Profit: ₱{totalProfit:N2}");
            Console.WriteLine($"Avg per Transaction: ₱{avgPerTransaction:N2}");
        }

        /// <summary>
        /// Toggle between daily and monthly view
        /// </summary>
        public void ToggleView()
        {
            showMonthlyView = !showMonthlyView;
            ConfigureDataGridView();
            LoadSalesData(filterStartDate, filterEndDate);
        }

        /// <summary>
        /// Set view mode explicitly
        /// </summary>
        public void SetViewMode(bool isMonthly)
        {
            showMonthlyView = isMonthly;
            ConfigureDataGridView();
            LoadSalesData(filterStartDate, filterEndDate);
        }

        /// <summary>
        /// Public method to filter data by date range
        /// </summary>
        public void FilterByDateRange(DateTime startDate, DateTime endDate)
        {
            filterStartDate = startDate;
            filterEndDate = endDate;
            LoadSalesData(filterStartDate, filterEndDate);
        }

        /// <summary>
        /// Public method to clear filters and show all data
        /// </summary>
        public void ClearFilters()
        {
            filterStartDate = null;
            filterEndDate = null;
            LoadSalesData(null, null);
        }

        /// <summary>
        /// Refresh data from database
        /// </summary>
        public void RefreshData()
        {
            LoadSalesData(filterStartDate, filterEndDate);
        }

        private void ExportPDFBtn_Click(object sender, EventArgs e)
        {
            var report = BuildReportForExport();
            if (report == null || report.Rows == null || report.Rows.Count == 0)
            {
                MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            bool exported = ReportCsvExporter.ExportReportTableToCsv(report);
            if (exported)
            {
                MessageBox.Show("Report exported to CSV successfully.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Get current displayed data for export
        /// </summary>
        public List<SalesSummaryReport> GetCurrentData()
        {
            return dgvCurrentStockReport.DataSource as List<SalesSummaryReport>;
        }

        public ReportTable BuildReportForExport()
        {
            var data = currentData ?? GetCurrentData();
            if (data == null || data.Count == 0)
            {
                return null;
            }

            var report = new ReportTable
            {
                Title = showMonthlyView ? "Monthly Sales Summary" : "Daily Sales Summary",
                Subtitle = GetDateRangeSubtitle() + " | View: " + (showMonthlyView ? "Monthly" : "Daily")
            };

            report.Headers.AddRange(new[]
            {
                showMonthlyView ? "Month" : "Date",
                "Transactions",
                "Quantity Sold",
                "Total Sales",
                "Total Profit",
                "Avg Sale / Transaction"
            });

            foreach (var item in data)
            {
                report.Rows.Add(new List<string>
                {
                    item.Date.ToString(showMonthlyView ? "yyyy-MM" : "yyyy-MM-dd"),
                    item.NoOfTransactions.ToString(CultureInfo.InvariantCulture),
                    item.TotalQuantitySold.ToString(CultureInfo.InvariantCulture),
                    item.TotalSales.ToString("N2", CultureInfo.InvariantCulture),
                    item.TotalProfit.ToString("N2", CultureInfo.InvariantCulture),
                    item.AvgSalePerTransaction.ToString("N2", CultureInfo.InvariantCulture)
                });
            }

            return report;
        }

        private string GetDateRangeSubtitle()
        {
            if (!filterStartDate.HasValue && !filterEndDate.HasValue)
            {
                return "All Dates";
            }

            string start = filterStartDate.HasValue ? filterStartDate.Value.ToString("yyyy-MM-dd") : "...";
            string end = filterEndDate.HasValue ? filterEndDate.Value.ToString("yyyy-MM-dd") : "...";
            return "Date Range: " + start + " - " + end;
        }

        /// <summary>
        /// Get summary statistics
        /// </summary>
        public SalesSummaryStats GetSummaryStats()
        {
            var data = GetCurrentData();
            if (data == null || data.Count == 0)
                return new SalesSummaryStats();

            return new SalesSummaryStats
            {
                TotalTransactions = data.Sum(x => x.NoOfTransactions),
                TotalQuantity = data.Sum(x => x.TotalQuantitySold),
                TotalSales = data.Sum(x => x.TotalSales),
                TotalProfit = data.Sum(x => x.TotalProfit),
                AvgSalePerTransaction = data.Sum(x => x.NoOfTransactions) > 0
                    ? data.Sum(x => x.TotalSales) / data.Sum(x => x.NoOfTransactions)
                    : 0,
                PeriodCount = data.Count,
                IsMonthlyView = showMonthlyView
            };
        }

        //    private void ExportPDFBtn_Click(object sender, EventArgs e)
        //    {
        //        try
        //        {
        //            var data = dgvCurrentStockReport.DataSource as List<SalesSummaryReport>;
        //            if (data == null || data.Count == 0)
        //            {
        //                MessageBox.Show("No data available to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                return;
        //            }

        //            string title = showMonthlyView ? "Monthly Sales Summary Report" : "Daily Sales Summary Report";
        //            bool exported = ReportPdfExporter.ExportSalesSummary(data, title, filterStartDate, filterEndDate);
        //            if (exported)
        //            {
        //                MessageBox.Show("Report exported to PDF successfully.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show($"Failed to export report: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //}

        /// <summary>
        /// Summary statistics helper class
        /// </summary>
        public class SalesSummaryStats
        {
            public int TotalTransactions { get; set; }
            public int TotalQuantity { get; set; }
            public decimal TotalSales { get; set; }
            public decimal TotalProfit { get; set; }
            public decimal AvgSalePerTransaction { get; set; }
            public int PeriodCount { get; set; }
            public bool IsMonthlyView { get; set; }
        }
    }
}