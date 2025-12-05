using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Supplier_Reports;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module
{
    public partial class SupplierReportsPanel : UserControl
    {
        private int currentPage = 1;
        private int totalPages = 3;

        private Guna2ComboBox exportScopeComboBox;
        private Guna2Button ExportCSVBtn;

        public SupplierReportsPanel()
        {
            InitializeComponent();
            this.Load += SupplierReportsPanel_Load;

            CreateExportControls();
        }

        // ------------------------------------------------------------
        // CREATE EXPORT CONTROLS (Top-right UI)
        // ------------------------------------------------------------
        private void CreateExportControls()
        {
            // === Export Scope Dropdown ===
            exportScopeComboBox = new Guna2ComboBox();
            exportScopeComboBox.Width = 200;
            exportScopeComboBox.Height = 35;
            exportScopeComboBox.Location = new Point(595, 10);
            exportScopeComboBox.BorderRadius = 8;
            exportScopeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            exportScopeComboBox.Font = new Font("Lexend SemiBold", 9F, FontStyle.Bold);

            exportScopeComboBox.Items.Add("Current Page Only");
            exportScopeComboBox.Items.Add("Export Entire Module");
            exportScopeComboBox.SelectedIndex = 0;

            Controls.Add(exportScopeComboBox);
            exportScopeComboBox.BringToFront();

            // === Export CSV Button ===
            ExportCSVBtn = new Guna2Button();
            ExportCSVBtn.Text = "Export CSV";
            ExportCSVBtn.Width = 135;
            ExportCSVBtn.Height = 35;
            ExportCSVBtn.Location = new Point(800, 10);
            ExportCSVBtn.FillColor = Color.FromArgb(0, 110, 196);
            ExportCSVBtn.ForeColor = Color.White;
            ExportCSVBtn.Font = new Font("Lexend SemiBold", 9, FontStyle.Bold);
            ExportCSVBtn.BorderRadius = 8;

            ExportCSVBtn.Click += ExportCSVBtn_Click;

            Controls.Add(ExportCSVBtn);
            ExportCSVBtn.BringToFront();
        }

        // ------------------------------------------------------------
        // PANEL LOAD
        // ------------------------------------------------------------
        private void SupplierReportsPanel_Load(object sender, EventArgs e)
        {
            ShowPage(currentPage);
            UpdatePaginationButtons();
        }

        // Page button clicks
        private void guna2Button5_Click(object sender, EventArgs e) => ShowPage(1);
        private void guna2Button2_Click(object sender, EventArgs e) => ShowPage(2);
        private void guna2Button1_Click(object sender, EventArgs e) => ShowPage(3);

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
                ShowPage(--currentPage);
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
                ShowPage(++currentPage);
        }

        // ------------------------------------------------------------
        // SHOW REPORT PAGE
        // ------------------------------------------------------------
        private void ShowPage(int page)
        {
            panel1.Controls.Clear();

            var ctrl = CreatePageControl(page);
            if (ctrl != null)
            {
                ctrl.Dock = DockStyle.Fill;
                panel1.Controls.Add(ctrl);
            }

            currentPage = page;
            UpdatePaginationButtons();
        }

        private UserControl CreatePageControl(int page)
        {
            switch (page)
            {
                case 1: return new SupplierPage1();
                case 2: return new SupplierPage2();
                case 3: return new SupplierPage3();
                default: return null;
            }
        }

        private void UpdatePaginationButtons()
        {
            guna2Button6.Enabled = currentPage > 1;          // <
            guna2Button4.Enabled = currentPage < totalPages;  // >
        }

        // ------------------------------------------------------------
        // EXPORT HELPERS
        // ------------------------------------------------------------
        private List<ReportTable> BuildModuleReportsForExport()
        {
            List<ReportTable> reports = new List<ReportTable>();

            for (int page = 1; page <= totalPages; page++)
            {
                IReportExportable ctrl = CreatePageControl(page) as IReportExportable;
                if (ctrl == null)
                    continue;

                ReportTable report = ctrl.BuildReportForExport();

                if (report != null && report.Rows != null && report.Rows.Count > 0)
                    reports.Add(report);
            }

            return reports;
        }

        // ------------------------------------------------------------
        // EXPORT BUTTON CLICK
        // ------------------------------------------------------------
        private void ExportCSVBtn_Click(object sender, EventArgs e)
        {
            if (panel1.Controls.Count == 0)
                return;

            IReportExportable exportable = panel1.Controls[0] as IReportExportable;

            if (exportable == null)
            {
                MessageBox.Show("This report page cannot be exported.",
                    "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool exportModule = exportScopeComboBox.SelectedIndex == 1;

            if (exportModule)
            {
                List<ReportTable> reports = BuildModuleReportsForExport();

                if (reports.Count == 0)
                {
                    MessageBox.Show("No data to export.",
                        "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                bool success = ReportCsvExporter2.ExportModule("Supplier", reports);

                if (success)
                {
                    MessageBox.Show("Module exported successfully!",
                        "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                return;
            }

            // Export ONLY current page
            ReportTable table = exportable.BuildReportForExport();

            if (table == null || table.Rows.Count == 0)
            {
                MessageBox.Show("No data to export.",
                    "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            bool exported = ReportCsvExporter2.ExportReportTable(table);

            if (exported)
            {
                MessageBox.Show("Report exported successfully!",
                    "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
