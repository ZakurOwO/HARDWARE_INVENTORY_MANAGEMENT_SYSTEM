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

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Reports_Module
{
    public partial class InventoryReportsPanel : UserControl
    {
        private int currentPage = 1;
        private int totalPages = 4;

        public InventoryReportsPanel()
        {
            InitializeComponent();
            this.Load += InventoryReportsPanel_Load;

            CreateExportCSVButton();
            CreateExportScopeComboBox();
        }
        private Guna.UI2.WinForms.Guna2Button ExportCSVBtn;
        private Guna.UI2.WinForms.Guna2ComboBox exportScopeComboBox;

        private void CreateExportCSVButton()
        {
            ExportCSVBtn = new Guna.UI2.WinForms.Guna2Button();

            // Similar properties to your existing Guna button
            ExportCSVBtn.Name = "ExportCSVBtn";
            ExportCSVBtn.Text = "Export CSV";

            ExportCSVBtn.Location = new Point(780, 7);
            ExportCSVBtn.Size = new Size(135, 35);

            ExportCSVBtn.Animated = false;
            ExportCSVBtn.AutoRoundedCorners = false;
            ExportCSVBtn.BorderRadius = 8;
            ExportCSVBtn.BorderThickness = 0;
            ExportCSVBtn.BorderColor = Color.Black; // (your screenshot shows BorderColor black)
            ExportCSVBtn.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;

            ExportCSVBtn.FillColor = Color.FromArgb(0, 110, 196);
            ExportCSVBtn.ForeColor = Color.White;

            ExportCSVBtn.Font = new Font("Lexend SemiBold", 9F, FontStyle.Bold);
            ExportCSVBtn.TextAlign = HorizontalAlignment.Center;

            ExportCSVBtn.UseTransparentBackground = false;
            ExportCSVBtn.Visible = true;
            ExportCSVBtn.Enabled = true;

            // Hook the click event to your export logic
            ExportCSVBtn.Click += ExportCSVBtn_Click;

            // Add it to the panel (IMPORTANT: choose the correct container)
            // If your page navigation buttons are on the same UserControl surface:
            this.Controls.Add(ExportCSVBtn);

            // If you have a top bar panel (like headerPanel), use that instead:
            // headerPanel.Controls.Add(ExportCSVBtn);

            ExportCSVBtn.BringToFront();
        }

        private void CreateExportScopeComboBox()
        {
            exportScopeComboBox = new Guna.UI2.WinForms.Guna2ComboBox();
            exportScopeComboBox.Name = "exportScopeComboBox";
            exportScopeComboBox.Items.AddRange(new object[] { "Export This Page", "Export Current Module" });
            exportScopeComboBox.SelectedIndex = 0;
            exportScopeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            exportScopeComboBox.Location = new Point(470, 7);
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
            exportScopeComboBox.BringToFront();
        }


        private void InventoryReportsPanel_Load(object sender, EventArgs e)
        {
            ShowPage(currentPage);
            UpdatePaginationButtons();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            // Page 1
            ShowPage(1);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            // Page 2
            ShowPage(2);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // Page 3
            ShowPage(3);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            // Page 4
            ShowPage(4);
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            // Previous page "<"
            if (currentPage > 1)
            {
                currentPage--;
                ShowPage(currentPage);
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            // Next page ">"
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
                    pageControl = CreatePageControl(1);
                    break;
                case 2:
                    pageControl = CreatePageControl(2);
                    break;
                case 3:
                    pageControl = CreatePageControl(3);
                    break;
                case 4:
                    pageControl = CreatePageControl(4);
                    break;
            }

            if (pageControl != null)
            {
                pageControl.Dock = DockStyle.Fill;
                panel1.Controls.Add(pageControl);
            }

            currentPage = page;
            UpdatePaginationButtons();
        }

        private void UpdatePaginationButtons()
        {
            // Update label
            label2.Text = $"Page {currentPage} of {totalPages}";

            // Reset all button styles
            ResetButtonStyles();

            // Highlight current page button
            switch (currentPage)
            {
                case 1:
                    HighlightButton(guna2Button5);
                    break;
                case 2:
                    HighlightButton(guna2Button2);
                    break;
                case 3:
                    HighlightButton(guna2Button1);
                    break;
                case 4:
                    HighlightButton(guna2Button3);
                    break;
            }

            // Enable/disable arrow buttons
            guna2Button6.Enabled = currentPage > 1;
            guna2Button4.Enabled = currentPage < totalPages;

            // Update arrow button styles
            UpdateArrowButtonStyle(guna2Button6, guna2Button6.Enabled);
            UpdateArrowButtonStyle(guna2Button4, guna2Button4.Enabled);
        }

        private void ResetButtonStyles()
        {
            // Reset all page number buttons to default style
            ResetButtonStyle(guna2Button5);
            ResetButtonStyle(guna2Button2);
            ResetButtonStyle(guna2Button1);
            ResetButtonStyle(guna2Button3);
        }

        private void ResetButtonStyle(Guna.UI2.WinForms.Guna2Button button)
        {
            button.FillColor = Color.Transparent;
            button.ForeColor = Color.FromArgb(29, 28, 35);
            button.BorderColor = Color.LightGray;
        }

        private void HighlightButton(Guna.UI2.WinForms.Guna2Button button)
        {
            button.FillColor = Color.FromArgb(0, 110, 196);
            button.ForeColor = Color.White;
            button.BorderColor = Color.FromArgb(0, 110, 196);
        }

        private void UpdateArrowButtonStyle(Guna.UI2.WinForms.Guna2Button button, bool enabled)
        {
            if (enabled)
            {
                button.FillColor = Color.Transparent;
                button.BorderColor = Color.LightGray;
            }
            else
            {
                button.FillColor = Color.FromArgb(240, 240, 240);
                button.BorderColor = Color.FromArgb(220, 220, 220);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Panel paint event
        }
        private void ExportCSVBtn_Click(object sender, EventArgs e)
        {
            if (panel1.Controls.Count == 0) return;

            var currentPage = panel1.Controls[0];

            if (!(currentPage is IReportExportable exportable))
            {
                MessageBox.Show("This report page does not support export yet.",
                    "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool exportModule = exportScopeComboBox != null && exportScopeComboBox.SelectedIndex == 1;

            if (exportModule)
            {
                Cursor.Current = Cursors.WaitCursor;
                List<ReportTable> moduleReports = BuildModuleReportsForExport();
                Cursor.Current = Cursors.Default;

                if (moduleReports == null || moduleReports.Count == 0)
                {
                    MessageBox.Show("No data to export.", "Export",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;
                bool exportedModule = ReportCsvExporter2.ExportModule("Inventory", moduleReports);
                Cursor.Current = Cursors.Default;

                if (exportedModule)
                {
                    // Success message handled by exporter
                }
            }
            else
            {
                Cursor.Current = Cursors.WaitCursor;
                ReportTable report = exportable.BuildReportForExport();
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



        private void ExportCSVBtn_Load(object sender, EventArgs e)
        {

        }

        private void mainButton1_Load(object sender, EventArgs e)
        {

        }

        private UserControl CreatePageControl(int page)
        {
            switch (page)
            {
                case 1:
                    return new InventoryPage1();
                case 2:
                    return new InventoryPage2();
                case 3:
                    return new InventoryPage3();
                case 4:
                    return new InventoryPage4();
                default:
                    return null;
            }
        }

        private List<ReportTable> BuildModuleReportsForExport()
        {
            List<ReportTable> reports = new List<ReportTable>();
            for (int page = 1; page <= totalPages; page++)
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