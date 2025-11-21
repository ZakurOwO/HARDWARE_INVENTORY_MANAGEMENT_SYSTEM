using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using Guna.UI2.WinForms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module
{
    public partial class Inventory_Pagination : UserControl
    {
        private PaginationHelper paginationHelper;
        private DataGridView targetDataGridView;

        public event EventHandler<int> PageChanged;

        public Inventory_Pagination()
        {
            InitializeComponent();
            WireUpHoverEvents();

           
        }

        private void WireUpHoverEvents()
        {
            // Individual hover events for navigation buttons
            GoleftButton.MouseEnter += GoleftButton_MouseEnter;
            GoleftButton.MouseLeave += GoleftButton_MouseLeave;

            GoRightButton.MouseEnter += GoRightButton_MouseEnter;
            GoRightButton.MouseLeave += GoRightButton_MouseLeave;
        }

        // Individual mouse enter events for navigation buttons
        private void GoleftButton_MouseEnter(object sender, EventArgs e)
        {
            if (GoleftButton.Enabled)
            {
                GoleftButton.Cursor = Cursors.Hand;
            }
        }

        private void GoleftButton_MouseLeave(object sender, EventArgs e)
        {
            GoleftButton.Cursor = Cursors.Default;
        }

        private void GoRightButton_MouseEnter(object sender, EventArgs e)
        {
            if (GoRightButton.Enabled)
            {
                GoRightButton.Cursor = Cursors.Hand;
            }
        }

        private void GoRightButton_MouseLeave(object sender, EventArgs e)
        {
            GoRightButton.Cursor = Cursors.Default;
        }

        public void InitializePagination(DataTable data, DataGridView dataGridView, int pageSize = 10)
        {
            try
            {
                targetDataGridView = dataGridView;
                paginationHelper = new PaginationHelper(data, pageSize);
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

        public void UpdateData(DataTable newData)
        {
            try
            {
                if (paginationHelper != null)
                {
                    paginationHelper.UpdateData(newData);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating pagination data: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdatePaginationDisplay()
        {
            if (paginationHelper == null) return;

            try
            {
                // Update page info label
                PaginationPageNumber.Text = paginationHelper.GetPageInfo();

                // Update navigation buttons
                GoleftButton.Enabled = (paginationHelper.CurrentPage > 1);
                GoRightButton.Enabled = (paginationHelper.CurrentPage < paginationHelper.TotalPages);

                // Update cursor for navigation buttons
                GoleftButton.Cursor = GoleftButton.Enabled ? Cursors.Hand : Cursors.Default;
                GoRightButton.Cursor = GoRightButton.Enabled ? Cursors.Hand : Cursors.Default;

                // Show/hide pagination based on whether there are pages
                this.Visible = (paginationHelper.TotalPages > 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating pagination display: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GoleftButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (paginationHelper != null)
                {
                    if (!paginationHelper.PreviousPage())
                    {
                        MessageBox.Show("You are already on the first page.", "First Page",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error navigating to previous page: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GoRightButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (paginationHelper != null)
                {
                    if (!paginationHelper.NextPage())
                    {
                        MessageBox.Show("You are already on the last page.", "Last Page",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error navigating to next page: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Public methods for external control
        public void GoToFirstPage()
        {
            try
            {
                paginationHelper?.FirstPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error going to first page: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void GoToLastPage()
        {
            try
            {
                paginationHelper?.LastPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error going to last page: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void GoToPage(int pageNumber)
        {
            try
            {
                if (paginationHelper != null && !paginationHelper.GoToPage(pageNumber))
                {
                    MessageBox.Show($"Page {pageNumber} is not available.", "Invalid Page",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error going to page {pageNumber}: {ex.Message}", "Error",
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

        private void Inventory_Pagination_Load(object sender, EventArgs e)
        {
            // Initialization handled in InitializePagination
        }

        // Remove the individual page button click events since we're not using them
        
    }
}