using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Sales_Report
{
    public partial class SalesPage1 : UserControl
    {
        private SalesReportDataAccess dataAccess;
        private DateTime? filterStartDate;
        private DateTime? filterEndDate;
        private Button btnExportPdf;

        public SalesPage1()
        {
            InitializeComponent();
            dataAccess = new SalesReportDataAccess();
            this.Load += SalesPage1_Load;
            ConfigureExportButton();
        }

        private void SalesPage1_Load(object sender, EventArgs e)
        {
            ConfigureDataGridView();
            LoadAllData();
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
            ProductID.DataPropertyName = "ProductID";
            ProductName.DataPropertyName = "ProductName";
            Category.DataPropertyName = "Category";
            QuantitySold.DataPropertyName = "QuantitySold";
            UnitPrice.DataPropertyName = "UnitPrice";
            TotalSales.DataPropertyName = "TotalSales";

            // Format currency columns
            UnitPrice.DefaultCellStyle.Format = "₱#,##0.00";
            TotalSales.DefaultCellStyle.Format = "₱#,##0.00";

            // Align numeric columns
            QuantitySold.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            UnitPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            TotalSales.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public void LoadAllData()
        {
            try
            {
                // Show loading indicator
                this.Cursor = Cursors.WaitCursor;

                // Load key metrics
                LoadKeyMetrics();

                // Load sales by product data
            LoadSalesData(filterStartDate, filterEndDate);

            this.Cursor = Cursors.Default;
        }
        catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show($"Error loading sales data: {ex.Message}",
                    "Data Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadKeyMetrics()
        {
            try
            {
                var metrics = dataAccess.GetKeyMetrics();

                // Update key metrics controls (only Title and Value properties exist)
                reportsKeyMetrics1.Title = "Total Revenue";
                reportsKeyMetrics1.Value = (int)metrics.TotalRevenue;

                reportsKeyMetrics2.Title = "Total Transactions";
                reportsKeyMetrics2.Value = metrics.TotalTransactions;

                reportsKeyMetrics3.Title = "Avg. Order Value";
                reportsKeyMetrics3.Value = (int)metrics.AvgOrderValue;

                reportsKeyMetrics4.Title = "Growth Rate";
                reportsKeyMetrics4.Value = (int)metrics.GrowthRate;

                // Change icon based on growth rate
                if (metrics.GrowthRate >= 0)
                {
                    reportsKeyMetrics4.Icon = Properties.Resources.Increase_Arrow_Icon;
                }
                else
                {
                    // Uncomment if you have a decrease arrow icon
                    // reportsKeyMetrics4.Icon = Properties.Resources.Decrease_Arrow_Icon;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading key metrics: {ex.Message}",
                    "Metrics Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadSalesData(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                // Get sales data from database
                var salesData = dataAccess.GetSalesByProduct(startDate, endDate);

                // Bind to DataGridView
                dgvCurrentStockReport.DataSource = null;
                dgvCurrentStockReport.DataSource = salesData;

                // Update row count or summary info if needed
                Console.WriteLine($"Loaded {salesData.Count} product sales records");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading sales data: {ex.Message}",
                    "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void ConfigureExportButton()
        {
            btnExportPdf = new Button
            {
                Text = "Export to PDF",
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                AutoSize = true,
                BackColor = Color.FromArgb(76, 175, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnExportPdf.FlatAppearance.BorderSize = 0;
            btnExportPdf.Location = new Point(this.Width - 180, 10);
         //   btnExportPdf.Click += BtnExportPdf_Click;
            this.Controls.Add(btnExportPdf);
            btnExportPdf.BringToFront();
            this.Resize += (s, e) =>
            {
                btnExportPdf.Location = new Point(this.Width - btnExportPdf.Width - 20, btnExportPdf.Location.Y);
            };
        }

        //private void BtnExportPdf_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var data = dgvCurrentStockReport.DataSource as List<SalesProductReport>;
        //        if (data == null || data.Count == 0)
        //        {
        //            MessageBox.Show("No data available to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            return;
        //        }

        //      //  bool exported = ReportPdfExporter.ExportSalesByProduct(data, "Sales by Product Report", filterStartDate, filterEndDate);
        //        if (exported)
        //        {
        //            MessageBox.Show("Report exported to PDF successfully.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Failed to export report: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        /// <summary>
        /// Refresh all data from database
        /// </summary>
        public void RefreshData()
        {
            LoadAllData();
        }

        /// <summary>
        /// Get current displayed data for export
        /// </summary>
        public List<SalesProductReport> GetCurrentData()
        {
            return dgvCurrentStockReport.DataSource as List<SalesProductReport>;
        }
    }
}