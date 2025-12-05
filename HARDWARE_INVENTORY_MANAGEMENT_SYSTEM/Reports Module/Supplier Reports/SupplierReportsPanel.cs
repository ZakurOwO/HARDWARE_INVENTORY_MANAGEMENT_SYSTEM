using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module.Customers_Report;
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
            this.Load += SupplierReportsPanel_load;

            ExportPDFBtn.ButtonName = "Export CSV";
            ExportPDFBtn.Click += ExportPDFBtn_Click;
            CreateExportScopeComboBox();
        }

        private Guna.UI2.WinForms.Guna2ComboBox exportScopeComboBox;

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            //Page 1
            ShowPage(1);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            //Page 2
            ShowPage(2);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            //Page 3
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
            UserControl pageControl = CreatePageControl(page);

            if (pageControl != null)
            {
                pageControl.Dock = DockStyle.Fill;
                panel1.Controls.Add(pageControl);
            }

            currentPage = page;
        }

        private void SupplierReportsPanel_load(object sender, EventArgs e)
        {
            ShowPage(currentPage);
        }

        private void mainButton2_Load(object sender, EventArgs e)
        {

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

            bool exportModule = exportScopeComboBox != null && exportScopeComboBox.SelectedIndex == 1;

            Cursor.Current = Cursors.WaitCursor;
            var report = exportModule ? BuildModuleReportForExport() : exportable.BuildReportForExport();
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

        private void CreateExportScopeComboBox()
        {
            exportScopeComboBox = new Guna.UI2.WinForms.Guna2ComboBox();
            exportScopeComboBox.Name = "supplierExportScopeComboBox";
            exportScopeComboBox.Items.AddRange(new object[] { "Export This Page", "Export Current Module" });
            exportScopeComboBox.SelectedIndex = 0;
            exportScopeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            exportScopeComboBox.Location = new Point(470, 16);
            exportScopeComboBox.Size = new Size(150, 30);
            exportScopeComboBox.Font = new Font("Lexend SemiBold", 9F, FontStyle.Bold);
            exportScopeComboBox.FillColor = Color.White;
            exportScopeComboBox.ForeColor = Color.FromArgb(29, 28, 35);
            exportScopeComboBox.ItemHeight = 24;
            exportScopeComboBox.BorderRadius = 8;
            exportScopeComboBox.BorderThickness = 1;
            exportScopeComboBox.BorderColor = Color.LightGray;
            exportScopeComboBox.DrawMode = DrawMode.OwnerDrawFixed;

            this.Controls.Add(exportScopeComboBox);
            exportScopeComboBox.BringToFront();
        }

        private UserControl CreatePageControl(int page)
        {
            switch (page)
            {
                case 1:
                    return new SupplierPage1();
                case 2:
                    return new SupplierPage2();
                case 3:
                    return new SupplierPage3();
                default:
                    return null;
            }
        }

        private ReportTable BuildModuleReportForExport()
        {
            var reports = new List<ReportTable>();
            for (int page = 1; page <= totalPages; page++)
            {
                var control = CreatePageControl(page) as IReportExportable;
                if (control == null)
                {
                    continue;
                }

                var report = control.BuildReportForExport();
                if (report != null && report.Rows != null && report.Rows.Count > 0)
                {
                    reports.Add(report);
                }
            }

            if (reports.Count == 0)
            {
                return null;
            }

            string subtitle = "Combined export generated on " + DateTime.Now.ToString("g");
            return ReportTableCombiner.BuildModuleReport("Supplier Module Reports", subtitle, reports);
        }
    }
}
