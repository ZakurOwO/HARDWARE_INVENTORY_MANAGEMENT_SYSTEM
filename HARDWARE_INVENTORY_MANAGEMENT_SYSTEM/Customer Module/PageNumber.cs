using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Customer_Module
{
    public partial class PageNumber : UserControl
    {
        private CustomerPaginationHelper paginationHelper;
        private DataGridView targetDataGridView;

        public event EventHandler<int> PageChanged;

        public PageNumber()
        {
            InitializeComponent();
            WireUpHoverEvents();
        }

        private void WireUpHoverEvents()
        {
            // Add hover effects for navigation buttons
            guna2Button4.MouseEnter += (s, e) => { if (guna2Button4.Enabled) guna2Button4.Cursor = Cursors.Hand; };
            guna2Button4.MouseLeave += (s, e) => guna2Button4.Cursor = Cursors.Default;
            guna2Button6.MouseEnter += (s, e) => { if (guna2Button6.Enabled) guna2Button6.Cursor = Cursors.Hand; };
            guna2Button6.MouseLeave += (s, e) => guna2Button6.Cursor = Cursors.Default;
        }

        public void InitializePagination(DataTable data, DataGridView dataGridView, int pageSize = 10)
        {
            try
            {
                targetDataGridView = dataGridView;
                paginationHelper = new CustomerPaginationHelper(data, pageSize);
                paginationHelper.PageChanged += PaginationHelper_PageChanged;

                UpdatePaginationDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing pagination: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PaginationHelper_PageChanged(object sender, EventArgs e)
        {
            UpdatePaginationDisplay();
            PageChanged?.Invoke(this, paginationHelper?.CurrentPage ?? 1);
        }

        private void UpdatePaginationDisplay()
        {
            if (paginationHelper == null) return;

            // Update navigation buttons
            guna2Button6.Enabled = (paginationHelper.CurrentPage > 1);    // Previous button
            guna2Button4.Enabled = (paginationHelper.CurrentPage < paginationHelper.TotalPages); // Next button
        }

        // Previous button click (guna2Button6 - left arrow)
        private void guna2Button6_Click(object sender, EventArgs e)
        {
            try
            {
                paginationHelper?.PreviousPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error navigating to previous page: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Next button click (guna2Button4 - right arrow)
        private void guna2Button4_Click(object sender, EventArgs e)
        {
            try
            {
                paginationHelper?.NextPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error navigating to next page: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DataTable GetCurrentPageData()
        {
            return paginationHelper?.GetCurrentPageData();
        }

        public void RefreshPagination()
        {
            UpdatePaginationDisplay();
        }

        public void UpdateData(DataTable newData)
        {
            paginationHelper?.UpdateData(newData);
        }

        private void PageNumber_Load(object sender, EventArgs e)
        {
            // Initialization code if needed
        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {
            // Paint code if needed
        }
    }
}