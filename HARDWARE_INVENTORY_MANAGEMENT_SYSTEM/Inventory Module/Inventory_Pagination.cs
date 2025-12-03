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
        private PaginationHelperCustomer paginationHelper;
        private DataGridView targetDataGridView;
        private bool _alwaysShowPagination = true;

        public event EventHandler<int> PageChanged;

        public bool AlwaysShowPagination
        {
            get => _alwaysShowPagination;
            set
            {
                _alwaysShowPagination = value;
                UpdateVisibility();
            }
        }

        public Inventory_Pagination()
        {
            InitializeComponent();
            WireUpHoverEvents();
            // Force visibility on load
            this.Visible = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // Ensure it's visible when loaded
            this.Visible = true;
            this.BringToFront();
        }

        private void WireUpHoverEvents()
        {
            GoleftButton.MouseEnter += GoleftButton_MouseEnter;
            GoleftButton.MouseLeave += GoleftButton_MouseLeave;
            GoRightButton.MouseEnter += GoRightButton_MouseEnter;
            GoRightButton.MouseLeave += GoRightButton_MouseLeave;
        }

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
                paginationHelper = new PaginationHelperCustomer(data, pageSize, _alwaysShowPagination);
                paginationHelper.PageChanged += PaginationHelper_PageChanged;

                UpdatePaginationDisplay();
                UpdateVisibility(); // Force update visibility
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing pagination: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Overload for simple initialization
        public void InitializePagination(int currentPage = 1, int totalPages = 1)
        {
            try
            {
                // Create empty data table for basic initialization
                var emptyData = new DataTable();
                emptyData.Columns.Add("product_name", typeof(string));

                paginationHelper = new PaginationHelperCustomer(emptyData, 10, _alwaysShowPagination);

                // Manually set page info
                if (paginationHelper != null)
                {
                    // We can't directly set these, but we can update with empty data
                    paginationHelper.PageChanged += PaginationHelper_PageChanged;
                }

                UpdatePaginationDisplay();
                UpdateVisibility(); // Force visibility
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
                    UpdateVisibility(); // Update visibility when data changes
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
            if (paginationHelper == null)
            {
                // Show basic info if no pagination helper
                PaginationPageNumber.Text = "Page 1 of 1";
                GoleftButton.Enabled = false;
                GoRightButton.Enabled = false;
                return;
            }

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

                UpdateVisibility(); // Always update visibility
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating pagination display: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateVisibility()
        {
            if (paginationHelper == null)
            {
                // If no data, still show if AlwaysShowPagination is true
                this.Visible = _alwaysShowPagination;
                return;
            }

            // Show if AlwaysShowPagination is true OR if there are multiple pages
            bool shouldShow = _alwaysShowPagination || paginationHelper.TotalPages > 1;
            this.Visible = shouldShow;

            if (shouldShow)
            {
                this.BringToFront();
            }
        }

        private void GoleftButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (paginationHelper != null)
                {
                    paginationHelper.PreviousPage();
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
                    paginationHelper.NextPage();
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
                if (paginationHelper != null)
                {
                    paginationHelper.GoToPage(pageNumber);
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
            UpdateVisibility();
        }

        public void ForceShow()
        {
            this.Visible = true;
            this.BringToFront();
        }

        public void ForceHide()
        {
            this.Visible = false;
        }

        private void Inventory_Pagination_Load(object sender, EventArgs e)
        {
            // Ensure visibility on load
            this.Visible = true;
            this.BringToFront();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            // Always bring to front when visible
            if (this.Visible)
            {
                this.BringToFront();
            }
        }
    }
}