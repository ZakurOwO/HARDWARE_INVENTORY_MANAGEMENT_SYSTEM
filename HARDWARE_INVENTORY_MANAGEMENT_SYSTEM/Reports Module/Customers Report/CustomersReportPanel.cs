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
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Customers_Report;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module
{
    public partial class CustomersReportPanel : UserControl
    {
        private CustomersPage1 customersPage;

        public CustomersReportPanel()
        {
            InitializeComponent();
            this.Load += CustomersReportPanel_load;
            CreateExportScopeComboBox();
        }

        private Guna2ComboBox exportScopeComboBox;

        private void ShowCustomerPage()
        {
            panel1.Controls.Clear();

            customersPage = new CustomersPage1();
            customersPage.Dock = DockStyle.Fill;
            panel1.Controls.Add(customersPage);
        }

        private void CustomersReportPanel_load(object sender, EventArgs e)
        {
            ShowCustomerPage();
        }

        private void mainButton1_Load(object sender, EventArgs e)
        {
            // Generate report button
        }

        private void mainButton1_Click(object sender, EventArgs e)
        {
            // Refresh all data from database
            if (customersPage != null)
            {
                customersPage.RefreshAllData();
            }
        }

        private void exportButton_Load(object sender, EventArgs e)
        {
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            if (customersPage == null)
            {
                MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            bool exportModule = exportScopeComboBox != null && exportScopeComboBox.SelectedIndex == 1;

            if (exportModule)
            {
                List<ReportTable> reports = BuildModuleReportsForExport();

                if (reports == null || reports.Count == 0)
                {
                    MessageBox.Show("No data to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                bool exportedModule = ReportCsvExporter2.ExportModule("Customers", reports);
                if (exportedModule)
                {
                    // Exporter handles confirmation
                }
            }
            else
            {
                ReportTable report = customersPage.BuildReportForExport();
                bool exported = ReportCsvExporter2.ExportReportTable(report);

                if (exported)
                {
                    MessageBox.Show("Report exported to CSV successfully.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void CreateExportScopeComboBox()
        {
            exportScopeComboBox = new Guna2ComboBox();
            exportScopeComboBox.Name = "exportScopeComboBox";
            exportScopeComboBox.Items.AddRange(new object[] { "Export This Page", "Export Current Module" });
            exportScopeComboBox.SelectedIndex = 0;
            exportScopeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            exportScopeComboBox.Location = new Point(606, 11);
            exportScopeComboBox.Size = new Size(151, 36);
            exportScopeComboBox.Font = new Font("Lexend SemiBold", 9F, FontStyle.Bold);
            exportScopeComboBox.FillColor = Color.White;
            exportScopeComboBox.ForeColor = Color.FromArgb(29, 28, 35);
            exportScopeComboBox.ItemHeight = 30;
            exportScopeComboBox.BorderRadius = 8;
            exportScopeComboBox.BorderThickness = 1;
            exportScopeComboBox.BorderColor = Color.LightGray;
            exportScopeComboBox.DrawMode = DrawMode.OwnerDrawFixed;

            this.Controls.Add(exportScopeComboBox);
            exportScopeComboBox.BringToFront();
        }

        private List<ReportTable> BuildModuleReportsForExport()
        {
            List<ReportTable> reports = new List<ReportTable>();
            if (customersPage != null)
            {
                ReportTable report = customersPage.BuildReportForExport();
                if (report != null && report.Rows != null && report.Rows.Count > 0)
                {
                    reports.Add(report);
                }
            }

            return reports;
        }

        // Remove all page navigation methods that are no longer needed:
        // - guna2Button5_Click
        // - guna2Button2_Click  
        // - guna2Button6_Click
        // - guna2Button4_Click
        // - ShowPage
        // - UpdatePageButtons
        // - StyleNavigationButtons
        // - UpdatePageInfo
    }
}