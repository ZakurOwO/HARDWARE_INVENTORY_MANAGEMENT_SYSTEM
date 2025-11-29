using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Sales_Report
{
    public partial class SalesPage2 : UserControl
    {
        private SalesReportDataAccess dataAccess;
        private DateTime? filterStartDate;
        private DateTime? filterEndDate;

        public SalesPage2()
        {
            InitializeComponent();
            dataAccess = new SalesReportDataAccess();
            this.Load += SalesPage2_Load;
        }

        private void SalesPage2_Load(object sender, EventArgs e)
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
            CustomerID.DataPropertyName = "CustomerID";
            CustomerName.DataPropertyName = "CustomerName";
            Contact.DataPropertyName = "Contact";
            TotalOrders.DataPropertyName = "TotalOrders";
            TotalQuantity.DataPropertyName = "TotalQuantity";
            TotalSales.DataPropertyName = "TotalSales";

            // Format currency columns
            TotalSales.DefaultCellStyle.Format = "₱#,##0.00";

            // Align numeric columns
            TotalOrders.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            TotalQuantity.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            TotalSales.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void LoadSalesData(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                // Show loading indicator
                this.Cursor = Cursors.WaitCursor;

                // Get sales data from database
                var salesData = dataAccess.GetSalesByCustomer(startDate, endDate);


                // Bind to DataGridView
                dgvCurrentStockReport.DataSource = null;
                dgvCurrentStockReport.DataSource = salesData;

                Console.WriteLine($"Loaded {salesData.Count} customer sales records");

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show($"Error loading customer sales data: {ex.Message}\n\n{ex.StackTrace}",
                    "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void FilterByDateRange(DateTime startDate, DateTime endDate)
        {
            filterStartDate = startDate;
            filterEndDate = endDate;
            LoadSalesData(filterStartDate, filterEndDate);
        }

        public void ClearFilters()
        {
            filterStartDate = null;
            filterEndDate = null;
            LoadSalesData(null, null);
        }

        public void RefreshData()
        {
            LoadSalesData(filterStartDate, filterEndDate);
        }

        public List<SalesCustomerReport> GetCurrentData()
        {
            return dgvCurrentStockReport.DataSource as List<SalesCustomerReport>;
        }

        public List<SalesCustomerReport> GetTopCustomers(int count = 10)
        {
            var data = GetCurrentData();
            if (data != null)
            {
                return data.OrderByDescending(x => x.TotalSales).Take(count).ToList();
            }
            return new List<SalesCustomerReport>();
        }
    }
}