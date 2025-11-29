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
    }
}