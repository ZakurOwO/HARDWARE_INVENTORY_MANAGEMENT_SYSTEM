using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Inventory_Report
{
    public partial class InventoryPage2 : UserControl, IReportExportable
    {
        private DataTable lowStockData;
        private DataTable expiryAlertData;
        private enum AlertSection
        {
            LowStock,
            Expiry
        }

        private AlertSection lastFocusedSection = AlertSection.LowStock;

        public InventoryPage2()
        {
            InitializeComponent();
            this.Load += InventoryPage2_Load;

            dgvCurrentStockReport.Enter += (_, __) => lastFocusedSection = AlertSection.LowStock;
            dgvCurrentStockReport.Click += (_, __) => lastFocusedSection = AlertSection.LowStock;
            guna2DataGridView1.Enter += (_, __) => lastFocusedSection = AlertSection.Expiry;
            guna2DataGridView1.Click += (_, __) => lastFocusedSection = AlertSection.Expiry;
        }

        private void InventoryPage2_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Load Low Stock Alerts
                LoadLowStockAlerts();

                // Load Expiry Alerts
                LoadExpiryAlerts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading alerts: {ex.Message}\n\n{ex.StackTrace}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadLowStockAlerts()
        {
            try
            {
                DataTable dt = GetLowStockAlerts();
                lowStockData = dt;

                // Clear existing rows
                dgvCurrentStockReport.Rows.Clear();

                if (dt.Rows.Count == 0)
                {
                    // Show message in the grid or just leave empty
                    lastFocusedSection = AlertSection.Expiry;
                    return;
                }

                // Populate the grid
                foreach (DataRow row in dt.Rows)
                {
                    dgvCurrentStockReport.Rows.Add(
                        row["ProductID"].ToString(),
                        row["product_name"].ToString(),
                        row["current_stock"].ToString(),
                        row["reorder_point"].ToString(),
                        row["Supplier"] != DBNull.Value ? row["Supplier"].ToString() : "N/A",
                        row["Status"].ToString()
                    );
                }

                lastFocusedSection = AlertSection.LowStock;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading low stock alerts: {ex.Message}");
            }
        }

        private DataTable GetLowStockAlerts()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString.DataSource))
            {
                string query = @"
                    SELECT DISTINCT
                        p.ProductID,
                        p.product_name,
                        p.current_stock,
                        p.reorder_point,
                        (SELECT TOP 1 s.supplier_name 
                         FROM PurchaseOrderItems poi 
                         INNER JOIN PurchaseOrders po ON poi.po_id = po.po_id 
                         INNER JOIN Suppliers s ON po.supplier_id = s.supplier_id 
                         WHERE poi.product_id = p.ProductInternalID 
                         ORDER BY po.po_date DESC) as Supplier,
                        CASE 
                            WHEN p.current_stock <= 0 THEN 'Critical'
                            WHEN p.current_stock <= (p.reorder_point * 0.5) THEN 'Very Low'
                            ELSE 'Low'
                        END as Status
                    FROM Products p
                    WHERE p.active = 1 
                    AND p.current_stock <= p.reorder_point
                    ORDER BY p.current_stock ASC";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        private void LoadExpiryAlerts()
        {
            try
            {
                DataTable dt = GetExpiryAlerts();
                expiryAlertData = dt;

                // Clear existing rows
                guna2DataGridView1.Rows.Clear();

                if (dt.Rows.Count == 0)
                {
                    // Show message or just leave empty
                    lastFocusedSection = AlertSection.LowStock;
                    return;
                }

                // Populate the grid
                foreach (DataRow row in dt.Rows)
                {
                    guna2DataGridView1.Rows.Add(
                        row["ProductID"].ToString(),
                        row["product_name"].ToString(),
                        row["Quantity"].ToString(),
                        Convert.ToDateTime(row["ExpiryDate"]).ToString("MM/dd/yyyy"),
                        row["DaysLeft"].ToString(),
                        row["Status"].ToString()
                    );
                }

                lastFocusedSection = AlertSection.Expiry;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading expiry alerts: {ex.Message}");
            }
        }

        private DataTable GetExpiryAlerts()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString.DataSource))
            {
                string query = @"
                    SELECT 
                        p.ProductID,
                        p.product_name,
                        SUM(pb.quantity_received) as Quantity,
                        pb.expiry_date as ExpiryDate,
                        DATEDIFF(DAY, GETDATE(), pb.expiry_date) as DaysLeft,
                        CASE 
                            WHEN DATEDIFF(DAY, GETDATE(), pb.expiry_date) <= 0 THEN 'Expired'
                            WHEN DATEDIFF(DAY, GETDATE(), pb.expiry_date) <= 7 THEN 'Critical'
                            WHEN DATEDIFF(DAY, GETDATE(), pb.expiry_date) <= 30 THEN 'Warning'
                            ELSE 'Normal'
                        END as Status
                    FROM ProductBatches pb
                    INNER JOIN Products p ON pb.product_id = p.ProductInternalID
                    WHERE pb.expiry_date IS NOT NULL
                    AND pb.expiry_date <= DATEADD(DAY, 30, GETDATE())
                    GROUP BY p.ProductID, p.product_name, pb.expiry_date
                    ORDER BY pb.expiry_date ASC";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public void RefreshData()
        {
            LoadData();
        }

        public ReportTable BuildReportForExport()
        {
            bool hasLowStock = lowStockData != null && lowStockData.Rows.Count > 0;
            bool hasExpiry = expiryAlertData != null && expiryAlertData.Rows.Count > 0;

            // Choose based on the last focused grid, but gracefully fall back if that dataset is empty
            if (lastFocusedSection == AlertSection.Expiry && hasExpiry)
            {
                return ReportTableFactory.FromDataTable(
                    expiryAlertData,
                    "Expiry Alerts",
                    "Products nearing or past expiry");
            }

            if (hasLowStock)
            {
                return ReportTableFactory.FromDataTable(
                    lowStockData,
                    "Low Stock Alerts",
                    "Products at or below reorder point");
            }

            if (hasExpiry)
            {
                return ReportTableFactory.FromDataTable(
                    expiryAlertData,
                    "Expiry Alerts",
                    "Products nearing or past expiry");
            }

            return null;
        }
    }
}