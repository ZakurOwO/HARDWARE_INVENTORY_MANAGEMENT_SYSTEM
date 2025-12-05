using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Customers_Report;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module
{
    public partial class CustomersReportPanel : UserControl
    {
        private CustomersPage1 customersPage;

        private Guna2ComboBox exportScopeComboBox;
        private Guna2Button exportCSVBtn;

        public CustomersReportPanel()
        {
            InitializeComponent();
            this.Load += CustomersReportPanel_Load;

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

        private void CustomersReportPanel_Load(object sender, EventArgs e)
        {
            ShowCustomerPage();
        }

        private void ShowCustomerPage()
        {
            panel1.Controls.Clear();

            customersPage = new CustomersPage1();
            customersPage.Dock = DockStyle.Fill;
            panel1.Controls.Add(customersPage);
        }

        private void mainButton1_Click(object sender, EventArgs e)
        {
            if (customersPage != null)
                customersPage.RefreshAllData();
        }

        private void ExportCSVBtn_Click(object sender, EventArgs e)
        {
            if (customersPage == null)
            {
                MessageBox.Show("No data to export.",
                    "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            bool exportModule = exportScopeComboBox.SelectedIndex == 1;

            ReportTable report;

            if (exportModule)
            {
                // Customers only has 1 page, but we still support module export
                report = BuildModuleReport();
            }
            else
            {
                report = customersPage.BuildReportForExport();
            }

            if (report == null || report.Rows == null || report.Rows.Count == 0)
            {
                MessageBox.Show("No data to export.",
                    "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            bool exported = ReportCsvExporter2.ExportReportTable(report);

            if (exported)
            {
                MessageBox.Show("Report exported successfully!",
                    "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private ReportTable BuildModuleReport()
        {
            // Supports module export (even though we have just one page)
            if (customersPage == null)
                return null;

            return customersPage.BuildReportForExport();
        }
    }
}
