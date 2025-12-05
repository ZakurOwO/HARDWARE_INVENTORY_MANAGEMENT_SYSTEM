using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Inventory_Report
{
    public partial class InventoryPage1 : UserControl
    {
        private InventoryReportsDataAccess dataAccess;

        public InventoryPage1()
        {
            InitializeComponent();
            dataAccess = new InventoryReportsDataAccess();
            this.Load += InventoryPage1_Load;
        }
        private static bool IsOnlyNewRow(DataGridView grid)
        {
            for (int i = 0; i < grid.Rows.Count; i++)
            {
                if (!grid.Rows[i].IsNewRow)
                    return false;
            }
            return true;
        }

            //private void ExportPDFBtn_Click(object sender, EventArgs e)
            //{
            //    var grid = FindDataGridView(reportsTable1);

            //    if (grid == null)
            //    {
            //        MessageBox.Show("No data grid available to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return;
            //    }

            //    if (grid.Rows.Count == 0 || IsOnlyNewRow(grid))

            //    {
            //        MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return;
            //    }

            //    bool exported = ReportPdfExporter.ExportDataGridView(grid, "Inventory Current Stock Report", null, null);
            //    if (exported)
            //    {
            //        MessageBox.Show("Report exported to PDF successfully.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //}

        private void InventoryPage1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Load metrics
                LoadMetrics();

                // Load table data - Get the DataGridView from reportsTable1
                LoadInventoryTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading inventory data: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadMetrics()
        {
            try
            {
                // Total Products
                int totalProducts = dataAccess.GetTotalProducts();
                reportsKeyMetrics1.Value = totalProducts;

                // Inventory Value
                decimal inventoryValue = dataAccess.GetInventoryValue();
                reportsKeyMetrics2.Value = (int)inventoryValue;

                // Low Stock Alert
                int lowStockCount = dataAccess.GetLowStockCount();
                reportsKeyMetrics3.Value = lowStockCount;

                // Expiry Alert
                int expiryAlertCount = dataAccess.GetExpiryAlertCount();
                reportsKeyMetrics4.Value = expiryAlertCount;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading metrics: {ex.Message}");
            }
        }

        private void LoadInventoryTable()
        {
            try
            {
                DataTable dt = GetCurrentStockReport();

                // Find the DataGridView in reportsTable1
                DataGridView dgv = FindDataGridView(reportsTable1);

                if (dgv != null)
                {
                    dgv.Rows.Clear();

                    foreach (DataRow row in dt.Rows)
                    {
                        dgv.Rows.Add(
                            row["SKU"].ToString(),
                            row["Item"].ToString(),
                            row["Category"].ToString(),
                            row["Stock"].ToString(),
                            row["AlertLevel"].ToString(),
                            row["Unit"].ToString(),
                            row["Price"].ToString()
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading table: {ex.Message}");
            }
        }

        private DataTable GetCurrentStockReport()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString.DataSource))
            {
                string query = @"
                    SELECT 
                        p.SKU,
                        p.product_name as Item,
                        c.category_name as Category,
                        p.current_stock as Stock,
                        p.reorder_point as AlertLevel,
                        u.unit_name as Unit,
                        p.SellingPrice as Price
                    FROM Products p
                    INNER JOIN Categories c ON p.category_id = c.CategoryID
                    INNER JOIN Units u ON p.unit_id = u.UnitID
                    WHERE p.active = 1
                    ORDER BY p.product_name";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        private DataGridView FindDataGridView(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is DataGridView dgv)
                    return dgv;

                if (control.HasChildren)
                {
                    var found = FindDataGridView(control);
                    if (found != null) return found;
                }
            }
            return null;
        }

        public void RefreshData()
        {
            LoadData();
        }
    }
}