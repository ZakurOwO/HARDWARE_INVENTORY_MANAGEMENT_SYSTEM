using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Inventory_Report;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Sales_Report;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module
{
    public partial class SalesPage : UserControl
    {
        private int currentPage = 1;
        private int totalPages = 3;

        public SalesPage()
        {
            InitializeComponent();
            this.Load += SalesReportPanel_load;

            ExportPDFBtn.ButtonName = "Export CSV";
            ExportPDFBtn.Click += ExportPDFBtn_Click;
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            //page 1
            ShowPage(1);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            //page 2
            ShowPage(2);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            //page 3
            ShowPage(3);
        }

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
            UserControl pageControl = null;

            switch (page)
            {
                case 1:
                    pageControl = new SalesPage1();
                    break;
                case 2:
                    pageControl = new SalesPage2();
                    break;
                case 3:
                    pageControl = new SalesPage3();
                    break;
            }

            if (pageControl != null)
            {
                pageControl.Dock = DockStyle.Fill;
                panel1.Controls.Add(pageControl);
            }

            currentPage = page;
        }

        private void SalesReportPanel_load(object sender, EventArgs e)
        {
            ShowPage(currentPage);
        }

        private void ExportPDFBtn_Click(object sender, EventArgs e)
        {
            if (panel1.Controls.Count == 0)
            {
                return;
            }

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

            bool exported = ReportCsvExporter.ExportReportTableToCsv(report);
            if (exported)
            {
                MessageBox.Show("Report exported to CSV successfully.", "Export",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
