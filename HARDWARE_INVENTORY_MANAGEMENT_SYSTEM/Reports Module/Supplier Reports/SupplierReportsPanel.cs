using System;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Supplier_Reports;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module
{
    public partial class SupplierReportsPanel : UserControl
    {
        private int currentPage = 1;
        private int totalPages = 3;

        public SupplierReportsPanel()
        {
            InitializeComponent();
            this.Load += SupplierReportsPanel_Load;

            ExportCSVBtn.ButtonName = "Export CSV";
        }

        private void SupplierReportsPanel_Load(object sender, EventArgs e)
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
                case 1: return new SupplierPage1();
                case 2: return new SupplierPage2();
                case 3: return new SupplierPage3();
                default: return null;
            }
        }

        private void UpdatePaginationButtons()
        {
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

            Cursor.Current = Cursors.WaitCursor;
            var report = exportable.BuildReportForExport();
            Cursor.Current = Cursors.Default;

            if (report == null || report.Rows == null || report.Rows.Count == 0)
            {
                MessageBox.Show("No data to export.", "Export",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            bool exported = ReportCsvExporter2.ExportReportTable(report);
            if (exported)
            {
                MessageBox.Show("Report exported to CSV successfully.", "Export",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
