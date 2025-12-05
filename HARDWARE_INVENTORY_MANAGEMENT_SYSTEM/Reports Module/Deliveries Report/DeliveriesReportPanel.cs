using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
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
        private Guna2ComboBox exportScopeComboBox;
        private Guna2Button ExportCSVBtn;

        public DeliveriesReportPanel()
        {
            InitializeComponent();
            deliveriesData = new DeliveriesDataAccess();
            this.Load += DeliveriesReportPanel_Load;

            // Setup pagination controls
            HidePaginationControls();

            CreateExportControls();

            // Wire up navigation buttons
            guna2Button6.Click += GunaPreviousButton_Click; // < button
            guna2Button4.Click += GunaNextButton_Click;     // > button
        }

        private void DeliveriesReportPanel_Load(object sender, EventArgs e)
        {
            LoadDeliveryReportData();
        }

        private void CreateExportControls()
        {
            ExportCSVBtn = new Guna2Button();
            ExportCSVBtn.Name = "ExportCSVBtn";
            ExportCSVBtn.Text = "Export CSV";
            ExportCSVBtn.Location = new Point(780, 11);
            ExportCSVBtn.Size = new Size(135, 35);
            ExportCSVBtn.BorderRadius = 8;
            ExportCSVBtn.FillColor = Color.FromArgb(0, 110, 196);
            ExportCSVBtn.ForeColor = Color.White;
            ExportCSVBtn.Font = new Font("Lexend SemiBold", 9F, FontStyle.Bold);
            ExportCSVBtn.Click += ExportCSVBtn_Click;
            ExportCSVBtn.BorderColor = Color.Black;

            exportScopeComboBox = new Guna2ComboBox();
            exportScopeComboBox.Name = "exportScopeComboBox";
            exportScopeComboBox.Items.AddRange(new object[] { "Export This Page", "Export Current Module" });
            exportScopeComboBox.SelectedIndex = 0;
            exportScopeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            exportScopeComboBox.Location = new Point(620, 11);
            exportScopeComboBox.Size = new Size(150, 35);
            exportScopeComboBox.Font = new Font("Lexend SemiBold", 9F, FontStyle.Bold);
            exportScopeComboBox.FillColor = Color.White;
            exportScopeComboBox.ForeColor = Color.FromArgb(29, 28, 35);
            exportScopeComboBox.ItemHeight = 30;
            exportScopeComboBox.BorderRadius = 8;
            exportScopeComboBox.BorderThickness = 1;
            exportScopeComboBox.BorderColor = Color.LightGray;
            exportScopeComboBox.DrawMode = DrawMode.OwnerDrawFixed;

            this.Controls.Add(exportScopeComboBox);
            this.Controls.Add(ExportCSVBtn);
            exportScopeComboBox.BringToFront();
            ExportCSVBtn.BringToFront();
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

        private void ExportCSVBtn_Click(object sender, EventArgs e)
        {
            bool exportModule = exportScopeComboBox != null && exportScopeComboBox.SelectedIndex == 1;

            ReportTable report;
            if (exportModule)
            {
                report = BuildFullReportForExport();
            }
            else
            {
                report = BuildCurrentPageReportForExport();
            }

            if (report == null || report.Rows == null || report.Rows.Count == 0)
            {
                MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            bool exported = ReportCsvExporter2.ExportReportTable(report);
            if (exported)
            {
                MessageBox.Show("Report exported to CSV successfully.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private ReportTable BuildCurrentPageReportForExport()
        {
            DataTable currentPageData = GetCurrentPageData();
            if (currentPageData == null || currentPageData.Rows.Count == 0)
            {
                return null;
            }

            return ReportTableFactory.FromDataTable(currentPageData, "Deliveries Summary Report", "Current page");
        }

        private ReportTable BuildFullReportForExport()
        {
            if (deliveriesDataTable == null || deliveriesDataTable.Rows.Count == 0)
            {
                return null;
            }

            return ReportTableFactory.FromDataTable(deliveriesDataTable, "Deliveries Summary Report", "All deliveries");
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