using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Deliveries_Report;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Data;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module
{
    public partial class DeliveriesReportPanel : UserControl
    {
        private DeliveriesDataAccess deliveriesData;
        private DataTable deliveriesDataTable;
        private int currentPage = 1;
        private int itemsPerPage = 10;
        private int totalPages = 1;

        public DeliveriesReportPanel()
        {
            InitializeComponent();
            deliveriesData = new DeliveriesDataAccess();
            this.Load += DeliveriesReportPanel_Load;

            // Setup pagination controls
            HidePaginationControls();

            // Wire up navigation buttons
            guna2Button6.Click += GunaPreviousButton_Click; // < button
            guna2Button4.Click += GunaNextButton_Click;     // > button
        }

        private void DeliveriesReportPanel_Load(object sender, EventArgs e)
        {
            LoadDeliveryReportData();
        }

        private void HidePaginationControls()
        {
            // Hide only page number buttons (1, 2)
            if (guna2Button2 != null) guna2Button2.Visible = false;
            if (guna2Button5 != null) guna2Button5.Visible = false;

            // Keep navigation arrows visible (<, >)
            if (guna2Button6 != null) guna2Button6.Visible = true; // < button
            if (guna2Button4 != null) guna2Button4.Visible = true; // > button

            // Keep the label2 (record count) visible
            if (label2 != null) label2.Visible = true;
        }

        private void LoadDeliveryReportData()
        {
            try
            {
                Console.WriteLine("=".PadRight(60, '='));
                Console.WriteLine("📊 LoadDeliveryReportData called");

                // Clear panel
                panel1.Controls.Clear();

                // Get all data first
                Console.WriteLine("🔍 Fetching data from database...");
                deliveriesDataTable = deliveriesData.GetDeliverySummary();

                if (deliveriesDataTable == null)
                {
                    Console.WriteLine("❌ GetDeliverySummary returned NULL!");
                    MessageBox.Show("Failed to retrieve delivery data from database.", "Database Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Console.WriteLine($"✓ Retrieved {deliveriesDataTable.Rows.Count} total records from database");
                Console.WriteLine($"✓ Columns: {string.Join(", ", deliveriesDataTable.Columns.Cast<DataColumn>().Select(c => c.ColumnName))}");

                // Calculate pagination
                if (deliveriesDataTable != null && deliveriesDataTable.Rows.Count > 0)
                {
                    totalPages = (int)Math.Ceiling((double)deliveriesDataTable.Rows.Count / itemsPerPage);
                    currentPage = Math.Max(1, Math.Min(currentPage, totalPages));
                    Console.WriteLine($"📄 Pagination: {totalPages} total pages, showing page {currentPage}");
                }
                else
                {
                    totalPages = 1;
                    currentPage = 1;
                    Console.WriteLine("⚠ No data for pagination");
                }

                // Create DeliveriesPage1 to display in the panel
                DeliveriesPage1 reportPage = new DeliveriesPage1();

                // Load current page data
                DataTable currentPageData = GetCurrentPageData();
                Console.WriteLine($"📋 Current page data: {currentPageData.Rows.Count} rows");

                reportPage.LoadDataDirectly(currentPageData);

                reportPage.Dock = DockStyle.Fill;
                panel1.Controls.Add(reportPage);

                // Update pagination UI
                UpdatePaginationUI();

                Console.WriteLine($"✓ Delivery report loaded - Page {currentPage} of {totalPages}");
                Console.WriteLine("=".PadRight(60, '='));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ ERROR in LoadDeliveryReportData: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                MessageBox.Show($"Error loading delivery report: {ex.Message}\n\nCheck console for details.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable GetCurrentPageData()
        {
            Console.WriteLine($"🔍 GetCurrentPageData called for page {currentPage}");

            if (deliveriesDataTable == null || deliveriesDataTable.Rows.Count == 0)
            {
                Console.WriteLine("⚠ No data available to paginate");
                return new DataTable();
            }

            DataTable pageData = deliveriesDataTable.Clone(); // Clone structure

            int startIndex = (currentPage - 1) * itemsPerPage;
            int endIndex = Math.Min(startIndex + itemsPerPage, deliveriesDataTable.Rows.Count);

            Console.WriteLine($"📄 Extracting rows {startIndex} to {endIndex - 1} (total: {deliveriesDataTable.Rows.Count})");

            for (int i = startIndex; i < endIndex; i++)
            {
                pageData.ImportRow(deliveriesDataTable.Rows[i]);
            }

            Console.WriteLine($"✓ Page data created with {pageData.Rows.Count} rows");

            return pageData;
        }

        private void UpdatePaginationUI()
        {
            // Update label
            int totalRecords = deliveriesDataTable?.Rows.Count ?? 0;
            int startRecord = totalRecords > 0 ? (currentPage - 1) * itemsPerPage + 1 : 0;
            int endRecord = Math.Min(currentPage * itemsPerPage, totalRecords);

            label2.Text = $"Showing {startRecord}-{endRecord} of {totalRecords} records (Page {currentPage}/{totalPages})";

            // Enable/disable navigation buttons
            guna2Button6.Enabled = currentPage > 1; // < button
            guna2Button4.Enabled = currentPage < totalPages; // > button

            // Update button appearance
            guna2Button6.FillColor = guna2Button6.Enabled ?
                Color.Transparent : Color.FromArgb(220, 220, 220);
            guna2Button4.FillColor = guna2Button4.Enabled ?
                Color.Transparent : Color.FromArgb(220, 220, 220);
        }

        private void GunaPreviousButton_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadDeliveryReportData();
            }
        }

        private void GunaNextButton_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                LoadDeliveryReportData();
            }
        }

        public void RefreshReport()
        {
            LoadDeliveryReportData();
        }

        // Optional: Add filter functionality if needed
        public void FilterByStatus(string status)
        {
            var reportPage = panel1.Controls.OfType<DeliveriesPage1>().FirstOrDefault();
            if (reportPage != null)
            {
                reportPage.FilterByStatus(status);
            }
        }

        public void FilterByDateRange(DateTime startDate, DateTime endDate)
        {
            var reportPage = panel1.Controls.OfType<DeliveriesPage1>().FirstOrDefault();
            if (reportPage != null)
            {
                reportPage.FilterByDateRange(startDate, endDate);
            }
        }
    }
}