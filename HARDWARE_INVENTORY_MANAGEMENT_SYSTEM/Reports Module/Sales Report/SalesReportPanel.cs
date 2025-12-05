using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Sales_Report;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module
{
    public partial class SalesReportPanel : UserControl
    {
        private int currentPage = 1;
        private int totalPages = 3;
        private Guna2ComboBox exportScopeComboBox;

        public SalesReportPanel()
        {
            InitializeComponent();
            this.Load += SalesReportPanel_Load;

            // Export button setup
            ExportCSVBtn.ButtonName = "Export CSV";
            CreateExportScopeComboBox();
        }

        private void SalesReportPanel_Load(object sender, EventArgs e)
        {
            ShowPage(currentPage);
            UpdatePaginationButtons();
        }

        private void guna2Button5_Click(object sender, EventArgs e) => ShowPage(1); // page 1
        private void guna2Button2_Click(object sender, EventArgs e) => ShowPage(2); // page 2
        private void guna2Button1_Click(object sender, EventArgs e) => ShowPage(3); // page 3

        private void guna2Button6_Click(object sender, EventArgs e) // "<"
        {
            if (currentPage > 1)
            {
                currentPage--;
                ShowPage(currentPage);
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e) // ">"
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                ShowPage(currentPage);
            }
        }

        private void ShowPage(int page)
        {
            panel1.Controls.Clear();

            UserControl pageControl = CreatePageControl(page);
            if (pageControl != null)
            {
                pageControl.Dock = DockStyle.Fill;
                panel1.Controls.Add(pageControl);
            }

            currentPage = page;
            UpdatePaginationButtons();
        }

        private UserControl CreatePageControl(int page)
        {
            // ✅ CHANGE THESE if your class names differ
            switch (page)
            {
                case 1: return new SalesPage1();
                case 2: return new SalesPage2();
                case 3: return new SalesPage3();
                default: return null;
            }
        }

        private void UpdatePaginationButtons()
        {
            // If you have a label: label2.Text = $"Page {currentPage} of {totalPages}";

            // Enable/disable arrow buttons (if present)
            guna2Button6.Enabled = currentPage > 1;
            guna2Button4.Enabled = currentPage < totalPages;
        }

        private void ExportCSVBtn_Click(object sender, EventArgs e)
        {
            if (panel1.Controls.Count == 0) return;

            var exportable = panel1.Controls[0] as IReportExportable;
            if (exportable == null)
            {
                MessageBox.Show("This report page does not support export yet.",
                    "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool exportModule = exportScopeComboBox != null && exportScopeComboBox.SelectedIndex == 1;

            if (exportModule)
            {
                Cursor.Current = Cursors.WaitCursor;
                List<ReportTable> reports = BuildModuleReportsForExport();
                Cursor.Current = Cursors.Default;

                if (reports == null || reports.Count == 0)
                {
                    MessageBox.Show("No data to export.", "Export",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;
                bool exportedModule = ReportCsvExporter2.ExportModule("Sales", reports);
                Cursor.Current = Cursors.Default;

                if (exportedModule)
                {
                    // Success message handled by exporter
                }
            }
            else
            {
                Cursor.Current = Cursors.WaitCursor;
                var report = exportable.BuildReportForExport();
                Cursor.Current = Cursors.Default;

                if (report == null || report.Rows == null || report.Rows.Count == 0)
                {
                    MessageBox.Show("No data to export.", "Export",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;
                bool exported = ReportCsvExporter2.ExportReportTable(report);
                Cursor.Current = Cursors.Default;

                if (exported)
                {
                    MessageBox.Show("Report exported to CSV successfully.", "Export",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

            bool exported = ReportCsvExporter2.ExportReportTable(report);
            if (exported)
            {
                IReportExportable control = CreatePageControl(page) as IReportExportable;
                if (control == null)
                {
                    continue;
                }

                ReportTable report = control.BuildReportForExport();
                if (report != null && report.Rows != null && report.Rows.Count > 0)
                {
                    reports.Add(report);
                }
            }

            return reports;
        }
    }
}
