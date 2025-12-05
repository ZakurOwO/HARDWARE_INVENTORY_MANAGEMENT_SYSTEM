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

            CreateExportPdfButton(); 
        }
        private Guna.UI2.WinForms.Guna2Button ExportPDFBtn;

        private void CreateExportPdfButton()
        {
            ExportPDFBtn = new Guna.UI2.WinForms.Guna2Button();

            // Similar properties to your existing Guna button
            ExportPDFBtn.Name = "ExportPDFBtn";
            ExportPDFBtn.Text = "Export PDF";

            ExportPDFBtn.Location = new Point(631, 7);
            ExportPDFBtn.Size = new Size(135, 35);

            ExportPDFBtn.Animated = false;
            ExportPDFBtn.AutoRoundedCorners = false;
            ExportPDFBtn.BorderRadius = 8;
            ExportPDFBtn.BorderThickness = 0;
            ExportPDFBtn.BorderColor = Color.Black; // (your screenshot shows BorderColor black)
            ExportPDFBtn.BorderStyle = System.Drawing.Drawing2D.DashStyle.Solid;

            ExportPDFBtn.FillColor = Color.FromArgb(0, 110, 196);
            ExportPDFBtn.ForeColor = Color.White;

            ExportPDFBtn.Font = new Font("Lexend SemiBold", 9F, FontStyle.Bold);
            ExportPDFBtn.TextAlign = HorizontalAlignment.Center;

            ExportPDFBtn.UseTransparentBackground = false;
            ExportPDFBtn.Visible = true;
            ExportPDFBtn.Enabled = true;

            // Hook the click event to your export logic
            ExportPDFBtn.Click += ExportPDFBtn_Click;

            // Add it to the panel (IMPORTANT: choose the correct container)
            // If your page navigation buttons are on the same UserControl surface:
            this.Controls.Add(ExportPDFBtn);

            // If you have a top bar panel (like headerPanel), use that instead:
            // headerPanel.Controls.Add(ExportPDFBtn);

            ExportPDFBtn.BringToFront();
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
                    pageControl = new InventoryPage1();
                    break;
                case 2:
                    pageControl = new InventoryPage2();
                    break;
                case 3:
                    pageControl = new InventoryPage3();
                    break;
                case 4:
                    pageControl = new InventoryPage4();
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
        private void ExportPDFBtn_Click(object sender, EventArgs e)
        {
            if (panel1.Controls.Count == 0) return;

            if (!(panel1.Controls[0] is InventoryPage1 page1))
            {
                MessageBox.Show("Export is only implemented for Page 1 right now.",
                    "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Build report (runs immediately on UI thread)
            Cursor.Current = Cursors.WaitCursor;
            var report = page1.GetCurrentReport();
            Cursor.Current = Cursors.Default;

            // Pick path (UI thread)
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "PDF Files (*.pdf)|*.pdf";
                sfd.Title = "Save Report as PDF";
                sfd.FileName = "InventoryReport.pdf"; // <- no date/time

                if (sfd.ShowDialog() != DialogResult.OK) return;

                // Export (runs immediately on UI thread)
                Cursor.Current = Cursors.WaitCursor;
                bool exported = ReportPdfExporter.ExportReportTableToPath(report, sfd.FileName);
                Cursor.Current = Cursors.Default;

                if (exported)
                {
                    MessageBox.Show("Report exported to PDF successfully.", "Export",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }



        private void ExportPDFBtn_Load(object sender, EventArgs e)
        {

        }

        private void mainButton1_Load(object sender, EventArgs e)
        {

        }
    }
}