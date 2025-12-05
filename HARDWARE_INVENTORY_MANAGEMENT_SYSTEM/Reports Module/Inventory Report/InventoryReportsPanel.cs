using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Inventory_Report;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module
{
    public partial class InventoryReportsPanel : UserControl
    {
        private int currentPage = 1;
        private int totalPages = 4;

        private Guna2ComboBox exportScopeComboBox;
        private Guna2Button exportCSVBtn;

        public InventoryReportsPanel()
        {
            InitializeComponent();
            this.Load += InventoryReportsPanel_Load;

            CreateExportControls();
        }

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
            exportCSVBtn = new Guna2Button();
            exportCSVBtn.Text = "Export CSV";
            exportCSVBtn.Width = 135;
            exportCSVBtn.Height = 35;
            exportCSVBtn.Location = new Point(800, 10);
            exportCSVBtn.FillColor = Color.FromArgb(0, 110, 196);
            exportCSVBtn.ForeColor = Color.White;
            exportCSVBtn.Font = new Font("Lexend SemiBold", 9, FontStyle.Bold);
            exportCSVBtn.BorderRadius = 8;

            exportCSVBtn.Click += ExportCSVBtn_Click;

            Controls.Add(exportCSVBtn);
            exportCSVBtn.BringToFront();
        }

        private void InventoryReportsPanel_Load(object sender, EventArgs e)
        {
            ShowPage(currentPage);
            UpdatePaginationButtons();
        }

        // Page buttons
        private void guna2Button5_Click(object sender, EventArgs e) => ShowPage(1);
        private void guna2Button2_Click(object sender, EventArgs e) => ShowPage(2);
        private void guna2Button1_Click(object sender, EventArgs e) => ShowPage(3);
        private void guna2Button3_Click(object sender, EventArgs e) => ShowPage(4);

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
                case 1: return new InventoryPage1();
                case 2: return new InventoryPage2();
                case 3: return new InventoryPage3();
                case 4: return new InventoryPage4();
            }

            return null;
        }

        private void UpdatePaginationButtons()
        {
            label2.Text = $"Page {currentPage} of {totalPages}";

            guna2Button6.Enabled = currentPage > 1;           // <
            guna2Button4.Enabled = currentPage < totalPages;   // >
        }

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

        private void ExportCSVBtn_Click(object sender, EventArgs e)
        {
            if (panel1.Controls.Count == 0)
                return;

            IReportExportable exportable = panel1.Controls[0] as IReportExportable;

            if (exportable == null)
            {
                MessageBox.Show("This report page cannot be exported.");
                return;
            }

            bool exportModule = exportScopeComboBox.SelectedIndex == 1;

            bool exported = false;

            if (exportModule)
            {
                List<ReportTable> reports = BuildModuleReportsForExport();

                if (reports.Count == 0)
                {
                    MessageBox.Show("No data to export.");
                    return;
                }

                exported = ReportCsvExporter2.ExportModule("Supplier", reports);
            }
            else
            {
                ReportTable table = exportable.BuildReportForExport();

                if (table == null || table.Rows.Count == 0)
                {
                    MessageBox.Show("No data to export.");
                    return;
                }

                exported = ReportCsvExporter2.ExportReportTable(table);
            }

            // ❌ Stop showing success when CANCEL is clicked!
            if (!exported)
                return;

            // ✔ Only show when SaveFileDialog result == OK
            MessageBox.Show("Report exported successfully!",
                "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
