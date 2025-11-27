using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Data;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Deliveries_Report
{
    public partial class DeliveriesPage1 : UserControl
    {
        //private DeliveriesDataAccess deliveriesData;
        //private PaginationHelper paginationHelper;
        //private DataTable deliveriesDataTable;

        //public DeliveriesPage1()
        //{
        //    InitializeComponent();
        //    deliveriesData = new DeliveriesDataAccess();
        //    InitializePagination();
        //    LoadAllData();
        //}

        //private void InitializePagination()
        //{
        //    paginationHelper = new PaginationHelper(new DataTable(), 10);
        //    paginationHelper.PageChanged += PaginationHelper_PageChanged;
        //}

        //private void LoadAllData()
        //{
        //    try
        //    {
        //        deliveriesDataTable = deliveriesData.GetDeliverySummary();
        //        paginationHelper.UpdateData(deliveriesDataTable);
        //        UpdatePaginationControls();
        //        DisplayCurrentPage();
        //        LoadKeyMetrics();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error loading delivery data: {ex.Message}", "Error",
        //            MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //public void RefreshAllData()
        //{
        //    LoadAllData();
        //    MessageBox.Show("Delivery report data has been refreshed successfully!", "Refresh Complete",
        //        MessageBoxButtons.OK, MessageBoxIcon.Information);
        //}

        //private void PaginationHelper_PageChanged(object sender, EventArgs e)
        //{
        //    DisplayCurrentPage();
        //    UpdatePaginationControls();
        //}

        //private void DisplayCurrentPage()
        //{
        //    try
        //    {
        //        DataTable currentPageData = paginationHelper.GetCurrentPageData();
        //        dgvCurrentStockReport.DataSource = currentPageData;
        //        FormatDeliverySummaryGrid();
        //        lblPageInfo.Text = paginationHelper.GetPageInfo();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error displaying page data: {ex.Message}", "Error",
        //            MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void FormatDeliverySummaryGrid()
        //{
        //    if (dgvCurrentStockReport.Columns.Count > 0)
        //    {
        //        dgvCurrentStockReport.Columns["DeliveryID"].HeaderText = "Delivery ID";
        //        dgvCurrentStockReport.Columns["DeliveryDate"].HeaderText = "Delivery Date";
        //        dgvCurrentStockReport.Columns["Customer"].HeaderText = "Customer";
        //        dgvCurrentStockReport.Columns["VehicleUsed"].HeaderText = "Vehicle Used";
        //        dgvCurrentStockReport.Columns["QuantityItems"].HeaderText = "Quantity / Items";
        //        dgvCurrentStockReport.Columns["Status"].HeaderText = "Status";

        //        // Format date column
        //        if (dgvCurrentStockReport.Columns["DeliveryDate"] != null)
        //        {
        //            dgvCurrentStockReport.Columns["DeliveryDate"].DefaultCellStyle.Format = "MM/dd/yyyy HH:mm";
        //        }

        //        // Color coding for status
        //        foreach (DataGridViewRow row in dgvCurrentStockReport.Rows)
        //        {
        //            if (row.Cells["Status"]?.Value != null)
        //            {
        //                string status = row.Cells["Status"].Value.ToString();
        //                row.DefaultCellStyle.BackColor = GetStatusColor(status);
        //            }
        //        }

        //        dgvCurrentStockReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        //    }
        //}

        //private Color GetStatusColor(string status)
        //{
        //    switch (status?.ToLower())
        //    {
        //        case "completed":
        //            return Color.FromArgb(220, 255, 220); // Light green
        //        case "in transit":
        //            return Color.FromArgb(255, 255, 200); // Light yellow
        //        case "scheduled":
        //            return Color.FromArgb(220, 230, 255); // Light blue
        //        case "cancelled":
        //            return Color.FromArgb(255, 220, 220); // Light red
        //        default:
        //            return Color.White;
        //    }
        //}

        //private void UpdatePaginationControls()
        //{
        //    // You'll need to add these controls to your designer
        //    // lblCurrentPage.Text = $"Page {paginationHelper.CurrentPage} of {paginationHelper.TotalPages}";

        //    // btnFirst.Enabled = paginationHelper.CurrentPage > 1;
        //    // btnPrevious.Enabled = paginationHelper.CurrentPage > 1;
        //    // btnNext.Enabled = paginationHelper.CurrentPage < paginationHelper.TotalPages;
        //    // btnLast.Enabled = paginationHelper.CurrentPage < paginationHelper.TotalPages;

        //    // StylePaginationButtons();
        //}

        //private void StylePaginationButtons()
        //{
        //    Color enabledColor = Color.FromArgb(0, 110, 196);
        //    Color disabledColor = Color.Gray;

        //    // Apply colors to buttons
        //    // You'll need to add these button controls to your designer
        //}

        //private void LoadKeyMetrics()
        //{
        //    try
        //    {
        //        int totalDeliveries = deliveriesData.GetTotalDeliveries();
        //        reportsKeyMetrics1.Value = totalDeliveries;
        //        reportsKeyMetrics1.Title = "Total Deliveries";

        //        int pendingDeliveries = deliveriesData.GetPendingDeliveries();
        //        reportsKeyMetrics2.Value = pendingDeliveries;
        //        reportsKeyMetrics2.Title = "Pending Deliveries";

        //        int totalVehicles = deliveriesData.GetTotalVehicles();
        //        reportsKeyMetrics3.Value = totalVehicles;
        //        reportsKeyMetrics3.Title = "Total Vehicles";

        //        int activeVehicles = deliveriesData.GetActiveVehicles();
        //        reportsKeyMetrics4.Value = activeVehicles;
        //        reportsKeyMetrics4.Title = "Active Vehicles";
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error loading key metrics: {ex.Message}", "Error",
        //            MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //// Add these methods for pagination button clicks
        //private void btnFirst_Click(object sender, EventArgs e)
        //{
        //    paginationHelper.FirstPage();
        //}

        //private void btnPrevious_Click(object sender, EventArgs e)
        //{
        //    paginationHelper.PreviousPage();
        //}

        //private void btnNext_Click(object sender, EventArgs e)
        //{
        //    paginationHelper.NextPage();
        //}

        //private void btnLast_Click(object sender, EventArgs e)
        //{
        //    paginationHelper.LastPage();
        //}

        //private void btnRefresh_Click(object sender, EventArgs e)
        //{
        //    LoadAllData();
        //}

        //private void DeliveriesPage1_Load(object sender, EventArgs e)
        //{
        //    LoadKeyMetrics();
        //    // StylePaginationButtons();
        //}
    }
}