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

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Inventory_Report
{
    public partial class InventoryPage4 : UserControl, IReportExportable
    {
        private InventoryReportsDataAccess dataAccess;
        private DataTable stockOutHistoryData;

        public InventoryPage4()
        {
            InitializeComponent();
            dataAccess = new InventoryReportsDataAccess();
            this.Load += InventoryPage4_Load;
        }

        private void InventoryPage4_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                LoadStockOutHistory();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading stock out history: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadStockOutHistory()
        {
            // Get stock out history for the last 30 days by default
            DateTime endDate = DateTime.Now;
            DateTime startDate = endDate.AddDays(-30);

            DataTable dt = dataAccess.GetStockOutHistory(startDate, endDate);
            stockOutHistoryData = dt;

            // Clear existing rows
            dgvCurrentStockReport.Rows.Clear();

            // Populate the grid
            foreach (DataRow row in dt.Rows)
            {
                dgvCurrentStockReport.Rows.Add(
                    row["ReferenceNo"].ToString(),
                    Convert.ToDateTime(row["Date"]).ToString("MM/dd/yyyy"),
                    row["ProductName"].ToString(),
                    row["QuantityOut"].ToString(),
                    row["Reason"].ToString(),
                    row["Remarks"].ToString()
                );
            }

            // Update label with count
            label2.Text = $"Stock Out History - {dt.Rows.Count} records (Last 30 days)";
        }

        public void RefreshData()
        {
            LoadData();
        }

        // Optional: Add date filter functionality
        public void LoadDataByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                DataTable dt = dataAccess.GetStockOutHistory(startDate, endDate);
                stockOutHistoryData = dt;

                dgvCurrentStockReport.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    dgvCurrentStockReport.Rows.Add(
                        row["ReferenceNo"].ToString(),
                        Convert.ToDateTime(row["Date"]).ToString("MM/dd/yyyy"),
                        row["ProductName"].ToString(),
                        row["QuantityOut"].ToString(),
                        row["Reason"].ToString(),
                        row["Remarks"].ToString()
                    );
                }

                label2.Text = $"Stock Out History - {dt.Rows.Count} records";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading stock out history: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public ReportTable BuildReportForExport()
        {
            if (stockOutHistoryData == null)
            {
                return null;
            }

            return ReportTableFactory.FromDataTable(
                stockOutHistoryData,
                "Stock Out History",
                label2.Text);
        }
    }
}