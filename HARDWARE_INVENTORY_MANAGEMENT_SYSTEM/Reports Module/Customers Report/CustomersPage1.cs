using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Data;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Customers_Report
{
    public partial class CustomersPage1 : UserControl, IReportExportable
    {
        private CustomerDataAccess customerData;
        private CustomerPaginationHelper paginationHelper;
        private DataTable customersData;
        private DataTable transactionDetailsData;

        public CustomersPage1()
        {
            InitializeComponent();
            customerData = new CustomerDataAccess();
            InitializePagination();
            LoadAllData();
        }

        private void InitializePagination()
        {
            paginationHelper = new CustomerPaginationHelper(new DataTable(), 10);
            paginationHelper.PageChanged += PaginationHelper_PageChanged;
        }

        private void LoadAllData()
        {
            try
            {
                customersData = customerData.GetCustomerPurchaseSummary();
                paginationHelper.UpdateData(customersData);
                UpdatePaginationControls();
                DisplayCurrentPage();

                transactionDetailsData = customerData.GetAllTransactionDetails();
                DisplayTransactionDetails();

                LoadKeyMetrics();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customer data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public ReportTable BuildReportForExport()
        {
            DataTable dt = customersData;
            return ReportTableFactory.FromDataTable(dt, "Customers Report", "Customer purchase summary");
        }

        public void RefreshAllData()
        {
            LoadAllData();
            MessageBox.Show("Customer report data has been refreshed successfully!", "Refresh Complete",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void PaginationHelper_PageChanged(object sender, EventArgs e)
        {
            DisplayCurrentPage();
            UpdatePaginationControls();
        }

        private void DisplayCurrentPage()
        {
            try
            {
                DataTable currentPageData = paginationHelper.GetCurrentPageData();
                dgvCurrentStockReport.DataSource = currentPageData;
                FormatCustomerSummaryGrid();
                lblPageInfo.Text = paginationHelper.GetPageInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying page data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayTransactionDetails()
        {
            try
            {
                guna2DataGridView1.DataSource = transactionDetailsData;
                FormatTransactionDetailsGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying transaction details: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatCustomerSummaryGrid()
        {
            if (dgvCurrentStockReport.Columns.Count > 0)
            {
                if (dgvCurrentStockReport.Columns["CustomerID"] != null)
                    dgvCurrentStockReport.Columns["CustomerID"].HeaderText = "Customer ID";

                if (dgvCurrentStockReport.Columns["customer_name"] != null)
                    dgvCurrentStockReport.Columns["customer_name"].HeaderText = "Customer Name";

                if (dgvCurrentStockReport.Columns["contact_number"] != null)
                    dgvCurrentStockReport.Columns["contact_number"].HeaderText = "Contact Number";

                if (dgvCurrentStockReport.Columns["address"] != null)
                    dgvCurrentStockReport.Columns["address"].HeaderText = "Address";

                if (dgvCurrentStockReport.Columns["transaction_count"] != null)
                    dgvCurrentStockReport.Columns["transaction_count"].HeaderText = "Total Transactions";

                if (dgvCurrentStockReport.Columns["total_spent"] != null)
                {
                    dgvCurrentStockReport.Columns["total_spent"].HeaderText = "Total Spent";
                    dgvCurrentStockReport.Columns["total_spent"].DefaultCellStyle.Format = "C2";
                    dgvCurrentStockReport.Columns["total_spent"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                if (dgvCurrentStockReport.Columns["last_purchase_date"] != null)
                {
                    dgvCurrentStockReport.Columns["last_purchase_date"].HeaderText = "Last Purchase";
                    dgvCurrentStockReport.Columns["last_purchase_date"].DefaultCellStyle.Format = "MM/dd/yyyy";
                }

                dgvCurrentStockReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void FormatTransactionDetailsGrid()
        {
            if (guna2DataGridView1.Columns.Count > 0)
            {
                if (guna2DataGridView1.Columns["TotalAmount"] != null)
                {
                    guna2DataGridView1.Columns["TotalAmount"].DefaultCellStyle.Format = "C2";
                    guna2DataGridView1.Columns["TotalAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                if (guna2DataGridView1.Columns["Date"] != null)
                {
                    guna2DataGridView1.Columns["Date"].DefaultCellStyle.Format = "MM/dd/yyyy HH:mm";
                }

                guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void UpdatePaginationControls()
        {
            lblCurrentPage.Text = $"Page {paginationHelper.CurrentPage} of {paginationHelper.TotalPages}";

            btnFirst.Enabled = paginationHelper.CurrentPage > 1;
            btnPrevious.Enabled = paginationHelper.CurrentPage > 1;
            btnNext.Enabled = paginationHelper.CurrentPage < paginationHelper.TotalPages;
            btnLast.Enabled = paginationHelper.CurrentPage < paginationHelper.TotalPages;

            StylePaginationButtons();
        }

        private void StylePaginationButtons()
        {
            Color enabledColor = Color.FromArgb(0, 110, 196);
            Color disabledColor = Color.Gray;

            btnFirst.FillColor = btnFirst.Enabled ? enabledColor : disabledColor;
            btnPrevious.FillColor = btnPrevious.Enabled ? enabledColor : disabledColor;
            btnNext.FillColor = btnNext.Enabled ? enabledColor : disabledColor;
            btnLast.FillColor = btnLast.Enabled ? enabledColor : disabledColor;
            btnRefresh.FillColor = enabledColor;
        }

        private void reportsKeyMetrics1_Load(object sender, EventArgs e)
        {
            int totalCustomers = customerData.GetTotalCustomers();
            reportsKeyMetrics1.Value = totalCustomers;
            reportsKeyMetrics1.Title = "Total Customers";
        }

        private void reportsKeyMetrics2_Load(object sender, EventArgs e)
        {
            decimal totalPurchases = customerData.GetTotalCustomerPurchases();
            reportsKeyMetrics2.Value = (int)Math.Round(totalPurchases);
            reportsKeyMetrics2.Title = "Total Revenue";
        }

        private void reportsKeyMetrics3_Load(object sender, EventArgs e)
        {
            int activeCustomers = customerData.GetActiveCustomers();
            reportsKeyMetrics3.Value = activeCustomers;
            reportsKeyMetrics3.Title = "Active Customers";
        }

        private void reportsKeyMetrics4_Load(object sender, EventArgs e)
        {
            decimal averageSpend = customerData.GetAverageCustomerSpend();
            reportsKeyMetrics4.Value = (int)Math.Round(averageSpend);
            reportsKeyMetrics4.Title = "Avg. Spend";
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            paginationHelper.FirstPage();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            paginationHelper.PreviousPage();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            paginationHelper.NextPage();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            paginationHelper.LastPage();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadAllData();
        }

        private void LoadKeyMetrics()
        {
            reportsKeyMetrics1_Load(null, EventArgs.Empty);
            reportsKeyMetrics2_Load(null, EventArgs.Empty);
            reportsKeyMetrics3_Load(null, EventArgs.Empty);
            reportsKeyMetrics4_Load(null, EventArgs.Empty);
        }

        private void CustomersPage1_Load(object sender, EventArgs e)
        {
            LoadKeyMetrics();
            StylePaginationButtons();
        }

        private void dgvCurrentStockReport_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle cell clicks if needed
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle transaction details grid clicks if needed
        }
    }
}