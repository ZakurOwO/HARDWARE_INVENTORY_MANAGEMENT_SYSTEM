using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Data;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Deliveries_Report
{
    public partial class DeliveriesPage1 : UserControl, IReportExportable
    {
        private DeliveriesDataAccess deliveriesData;
        private DataTable deliveriesDataTable;

        public DeliveriesPage1()
        {
            InitializeComponent();
            deliveriesData = new DeliveriesDataAccess();

            // Initialize on load
            this.Load += DeliveriesPage1_Load;
            ExportCSVBtn.Text = "Export CSV";

        }

        private void DeliveriesPage1_Load(object sender, EventArgs e)
        {
            // Only load key metrics on initial load
            // Data will be loaded via LoadDataDirectly() from parent
            LoadKeyMetrics();
        }

        // New method to load data directly (for pagination)
        public void LoadDataDirectly(DataTable data)
        {
            try
            {
                Console.WriteLine($"📊 LoadDataDirectly called with {data?.Rows.Count ?? 0} rows");

                if (data == null)
                {
                    Console.WriteLine("❌ Data is NULL!");
                    dgvCurrentStockReport.DataSource = null;
                    return;
                }

                if (data.Rows.Count == 0)
                {
                    Console.WriteLine("⚠ DataTable has 0 rows!");
                    dgvCurrentStockReport.DataSource = null;
                    MessageBox.Show("No delivery records found.", "No Data",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Log column names in the DataTable
                Console.WriteLine($"DataTable columns: {string.Join(", ", data.Columns.Cast<DataColumn>().Select(c => c.ColumnName))}");

                // Log first row data
                if (data.Rows.Count > 0)
                {
                    var firstRow = data.Rows[0];
                    Console.WriteLine($"First row data:");
                    foreach (DataColumn col in data.Columns)
                    {
                        Console.WriteLine($"  {col.ColumnName} = {firstRow[col]}");
                    }
                }

                deliveriesDataTable = data;

                // Bind data to grid
                dgvCurrentStockReport.DataSource = null; // Clear first
                dgvCurrentStockReport.DataSource = deliveriesDataTable;

                Console.WriteLine($"✓ Data bound to grid. Grid now has {dgvCurrentStockReport.Rows.Count} rows");

                // Format the grid
                FormatDeliverySummaryGrid();

                Console.WriteLine("✓ Data successfully displayed in grid");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error loading data: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        

        private void ExportCSVBtn_Click(object sender, EventArgs e)
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

     

        private void LoadAllData()
        {
            try
            {
                Console.WriteLine("📊 Loading delivery report data...");

                // Load key metrics first
                LoadKeyMetrics();

                // Load delivery summary data into the grid
                deliveriesDataTable = deliveriesData.GetDeliverySummary();

                if (deliveriesDataTable != null && deliveriesDataTable.Rows.Count > 0)
                {
                    Console.WriteLine($"✓ Retrieved {deliveriesDataTable.Rows.Count} delivery records from database");

                    // Bind data to grid
                    dgvCurrentStockReport.DataSource = deliveriesDataTable;

                    // Format the grid
                    FormatDeliverySummaryGrid();

                    Console.WriteLine("✓ Data successfully displayed in grid");
                }
                else
                {
                    Console.WriteLine("⚠ No delivery records found in database");
                    MessageBox.Show("No delivery records found in the database.", "No Data",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error loading delivery data: {ex.Message}");
                MessageBox.Show($"Error loading delivery data: {ex.Message}\n\nPlease check:\n- Database connection\n- Table structure\n- SQL permissions",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatDeliverySummaryGrid()
        {
            try
            {
                if (dgvCurrentStockReport.Columns.Count == 0)
                {
                    Console.WriteLine("⚠ No columns found in DataGridView");
                    return;
                }

                Console.WriteLine($"📋 Formatting grid with {dgvCurrentStockReport.Columns.Count} columns");
                Console.WriteLine($"📋 Total rows: {dgvCurrentStockReport.Rows.Count}");

                // Format each column
                foreach (DataGridViewColumn col in dgvCurrentStockReport.Columns)
                {
                    Console.WriteLine($"  - Column: {col.Name}");
                }

                dgvCurrentStockReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Set column headers and widths
                if (dgvCurrentStockReport.Columns.Contains("DeliveryID"))
                {
                    dgvCurrentStockReport.Columns["DeliveryID"].HeaderText = "Delivery ID";
                    dgvCurrentStockReport.Columns["DeliveryID"].Width = 100;
                    dgvCurrentStockReport.Columns["DeliveryID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                if (dgvCurrentStockReport.Columns.Contains("DeliveryDate"))
                {
                    dgvCurrentStockReport.Columns["DeliveryDate"].HeaderText = "Delivery Date";
                    dgvCurrentStockReport.Columns["DeliveryDate"].Width = 120;
                    dgvCurrentStockReport.Columns["DeliveryDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                if (dgvCurrentStockReport.Columns.Contains("Customer"))
                {
                    dgvCurrentStockReport.Columns["Customer"].HeaderText = "Customer";
                    dgvCurrentStockReport.Columns["Customer"].Width = 180;
                }

                if (dgvCurrentStockReport.Columns.Contains("VehicleUsed"))
                {
                    dgvCurrentStockReport.Columns["VehicleUsed"].HeaderText = "Vehicle Used";
                    dgvCurrentStockReport.Columns["VehicleUsed"].Width = 130;
                    dgvCurrentStockReport.Columns["VehicleUsed"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                if (dgvCurrentStockReport.Columns.Contains("QuantityItems"))
                {
                    dgvCurrentStockReport.Columns["QuantityItems"].HeaderText = "Items";
                    dgvCurrentStockReport.Columns["QuantityItems"].Width = 80;
                    dgvCurrentStockReport.Columns["QuantityItems"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                if (dgvCurrentStockReport.Columns.Contains("Status"))
                {
                    dgvCurrentStockReport.Columns["Status"].HeaderText = "Status";
                    dgvCurrentStockReport.Columns["Status"].Width = 120;
                    dgvCurrentStockReport.Columns["Status"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                // Apply color coding for status column
                ApplyStatusColors();

                // Log first few rows of data for debugging
                int rowsToLog = Math.Min(3, dgvCurrentStockReport.Rows.Count);
                for (int i = 0; i < rowsToLog; i++)
                {
                    var row = dgvCurrentStockReport.Rows[i];
                    Console.WriteLine($"  Row {i}: DeliveryID={row.Cells["DeliveryID"].Value}, Customer={row.Cells["Customer"].Value}, Status={row.Cells["Status"].Value}");
                }

                // Auto-size columns to fill remaining space
                dgvCurrentStockReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                Console.WriteLine("✓ Grid formatting completed");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error in FormatDeliverySummaryGrid: {ex.Message}");
            }
        }

        private void ApplyStatusColors()
        {
            try
            {
                foreach (DataGridViewRow row in dgvCurrentStockReport.Rows)
                {
                    if (row.Cells["Status"]?.Value != null)
                    {
                        string status = row.Cells["Status"].Value.ToString().ToLower();
                        Color rowColor = GetStatusColor(status);

                        // Apply color to the entire row
                        row.DefaultCellStyle.BackColor = rowColor;

                        // Make text color darker for better readability
                        row.DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);

                        Console.WriteLine($"  Applied {rowColor.Name} to status: {status}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error applying status colors: {ex.Message}");
            }
        }

        private Color GetStatusColor(string status)
        {
            switch (status?.ToLower())
            {
                case "completed":
                case "delivered":
                    return Color.FromArgb(220, 255, 220); // Light green

                case "in transit":
                case "on the way":
                    return Color.FromArgb(255, 255, 200); // Light yellow

                case "scheduled":
                case "pending":
                    return Color.FromArgb(220, 230, 255); // Light blue

                case "cancelled":
                case "failed":
                    return Color.FromArgb(255, 220, 220); // Light red

                default:
                    return Color.White;
            }
        }

        private void LoadKeyMetrics()
        {
            try
            {
                Console.WriteLine("📈 Loading key metrics...");

                // Total Deliveries
                int totalDeliveries = deliveriesData.GetTotalDeliveries();
                reportsKeyMetrics1.Value = totalDeliveries;
                reportsKeyMetrics1.Title = "Total Deliveries";
                Console.WriteLine($"  Total Deliveries: {totalDeliveries}");

                // Pending Deliveries
                int pendingDeliveries = deliveriesData.GetPendingDeliveries();
                reportsKeyMetrics2.Value = pendingDeliveries;
                reportsKeyMetrics2.Title = "Pending Deliveries";
                Console.WriteLine($"  Pending Deliveries: {pendingDeliveries}");

                // Total Vehicles
                int totalVehicles = deliveriesData.GetTotalVehicles();
                reportsKeyMetrics3.Value = totalVehicles;
                reportsKeyMetrics3.Title = "Total Vehicles";
                Console.WriteLine($"  Total Vehicles: {totalVehicles}");

                // Active Vehicles
                int activeVehicles = deliveriesData.GetActiveVehicles();
                reportsKeyMetrics4.Value = activeVehicles;
                reportsKeyMetrics4.Title = "Active Vehicles";
                Console.WriteLine($"  Active Vehicles: {activeVehicles}");

                Console.WriteLine("✓ Key metrics loaded successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error loading key metrics: {ex.Message}");
                MessageBox.Show($"Error loading key metrics: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void RefreshAllData()
        {
            LoadAllData();
            MessageBox.Show("Delivery report data has been refreshed successfully!", "Refresh Complete",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Filter by status
        public void FilterByStatus(string status)
        {
            try
            {
                Console.WriteLine($"🔍 Filtering by status: {status}");

                if (string.IsNullOrEmpty(status) || status.ToLower() == "all")
                {
                    LoadAllData();
                }
                else
                {
                    deliveriesDataTable = deliveriesData.GetDeliveriesByStatus(status);
                    dgvCurrentStockReport.DataSource = deliveriesDataTable;
                    FormatDeliverySummaryGrid();

                    Console.WriteLine($"✓ Filtered to {deliveriesDataTable.Rows.Count} records with status: {status}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error filtering by status: {ex.Message}");
                MessageBox.Show($"Error filtering by status: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Filter by date range
        public void FilterByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                Console.WriteLine($"🔍 Filtering by date range: {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}");

                deliveriesDataTable = deliveriesData.GetDeliveriesByDateRange(startDate, endDate);
                dgvCurrentStockReport.DataSource = deliveriesDataTable;
                FormatDeliverySummaryGrid();

                Console.WriteLine($"✓ Filtered to {deliveriesDataTable.Rows.Count} records in date range");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error filtering by date range: {ex.Message}");
                MessageBox.Show($"Error filtering by date range: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Get current data count
        public int GetRecordCount()
        {
            return deliveriesDataTable?.Rows.Count ?? 0;
        }

        public ReportTable BuildReportForExport()
        {
            DataTable sourceTable = deliveriesDataTable ?? dgvCurrentStockReport.DataSource as DataTable;
            if (sourceTable == null || sourceTable.Rows.Count == 0)
            {
                return null;
            }

            return ReportTableFactory.FromDataTable(sourceTable, "Deliveries Summary Report", "All deliveries");
        }

        // Get summary statistics
        public string GetSummaryStatistics()
        {
            try
            {
                if (deliveriesDataTable == null || deliveriesDataTable.Rows.Count == 0)
                    return "No data available";

                int totalRecords = deliveriesDataTable.Rows.Count;
                int completedCount = 0;
                int pendingCount = 0;
                int inTransitCount = 0;
                int cancelledCount = 0;

                foreach (DataRow row in deliveriesDataTable.Rows)
                {
                    string status = row["Status"].ToString().ToLower();

                    if (status.Contains("completed") || status.Contains("delivered"))
                        completedCount++;
                    else if (status.Contains("pending") || status.Contains("scheduled"))
                        pendingCount++;
                    else if (status.Contains("transit") || status.Contains("way"))
                        inTransitCount++;
                    else if (status.Contains("cancelled") || status.Contains("failed"))
                        cancelledCount++;
                }

                return $"Total: {totalRecords} | Completed: {completedCount} | Pending: {pendingCount} | In Transit: {inTransitCount} | Cancelled: {cancelledCount}";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting statistics: {ex.Message}");
                return "Statistics unavailable";
            }
        }
    }
}