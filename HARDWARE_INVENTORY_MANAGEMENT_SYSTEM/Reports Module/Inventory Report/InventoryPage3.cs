using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Inventory_Report
{
    public partial class InventoryPage3 : UserControl, IReportExportable
    {
        private DataTable stockInHistoryData;

        public InventoryPage3()
        {
            InitializeComponent();
            this.Load += InventoryPage3_Load;
        }

        private void InventoryPage3_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                LoadStockInHistory();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading stock in history: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadStockInHistory()
        {
            try
            {
                // Get stock in history for the last 30 days by default
                DateTime endDate = DateTime.Now;
                DateTime startDate = endDate.AddDays(-30);

                DataTable dt = GetStockInHistory(startDate, endDate);
                stockInHistoryData = dt;

                // Clear existing rows
                dgvCurrentStockReport.Rows.Clear();

                if (dt.Rows.Count == 0)
                {
                    label2.Text = "Stock In History - No records found";
                    return;
                }

                // Populate the grid
                foreach (DataRow row in dt.Rows)
                {
                    dgvCurrentStockReport.Rows.Add(
                        row["ReferenceNo"].ToString(),
                        Convert.ToDateTime(row["Date"]).ToString("MM/dd/yyyy"),
                        row["ProductName"].ToString(),
                        row["Supplier"].ToString(),
                        row["QuantityIn"].ToString(),
                        row["Remarks"].ToString()
                    );
                }

                // Update label with count
                label2.Text = $"Stock In History - {dt.Rows.Count} records (Last 30 days)";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}\n\n{ex.StackTrace}");
            }
        }

        private DataTable GetStockInHistory(DateTime startDate, DateTime endDate)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString.DataSource))
            {
                string query = @"
                    SELECT 
                        d.DeliveryID as ReferenceNo,
                        d.delivery_date as Date,
                        p.product_name as ProductName,
                        ISNULL(s.supplier_name, 'N/A') as Supplier,
                        di.quantity_received as QuantityIn,
                        CASE 
                            WHEN d.status = 'Completed' THEN 'Received'
                            WHEN d.status = 'Scheduled' THEN 'Pending'
                            ELSE d.status
                        END as Remarks
                    FROM DeliveryItems di
                    INNER JOIN Deliveries d ON di.delivery_id = d.delivery_id
                    INNER JOIN Products p ON di.product_id = p.ProductInternalID
                    LEFT JOIN PurchaseOrders po ON d.po_id = po.po_id
                    LEFT JOIN Suppliers s ON po.supplier_id = s.supplier_id
                    WHERE d.delivery_type = 'PO_Delivery'
                    AND d.delivery_date >= @StartDate 
                    AND d.delivery_date <= @EndDate
                    ORDER BY d.delivery_date DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@EndDate", endDate);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public void RefreshData()
        {
            LoadData();
        }

        public void LoadDataByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                DataTable dt = GetStockInHistory(startDate, endDate);
                stockInHistoryData = dt;

                dgvCurrentStockReport.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    dgvCurrentStockReport.Rows.Add(
                        row["ReferenceNo"].ToString(),
                        Convert.ToDateTime(row["Date"]).ToString("MM/dd/yyyy"),
                        row["ProductName"].ToString(),
                        row["Supplier"].ToString(),
                        row["QuantityIn"].ToString(),
                        row["Remarks"].ToString()
                    );
                }

                label2.Text = $"Stock In History - {dt.Rows.Count} records";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading stock in history: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public ReportTable BuildReportForExport()
        {
            if (stockInHistoryData == null)
            {
                return null;
            }

            return ReportTableFactory.FromDataTable(
                stockInHistoryData,
                "Stock In History",
                label2.Text);
        }
    }
}