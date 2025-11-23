using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries
{
    public partial class Pagination_Deliveries : UserControl
    {
        // Events
        public event EventHandler<int> PageChanged;

        // Properties
        private int currentPage = 1;
        private int totalPages = 1;
        private int pageSize = 6; // 2 rows × 3 items per row
        private int totalRecords = 0;
        private DataTable dataSource;

        public Pagination_Deliveries()
        {
            InitializeComponent();
            FixButtonImages(); // Fix the button images
            UpdatePaginationDisplay();
        }

        // Fix the button images
        private void FixButtonImages()
        {
            try
            {
                // Flip the left button image to make it point left
                var leftImage = HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.direction_right_01;
                var flippedImage = new Bitmap(leftImage);
                flippedImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
                GoleftButton.BackgroundImage = flippedImage;

                // Keep the right button as is
                GoRightButton.BackgroundImage = HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties.Resources.direction_right_01;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fixing button images: {ex.Message}");
                // Fallback - use text if images fail
                GoleftButton.Text = "◀";
                GoRightButton.Text = "▶";
            }
        }

        #region Public Methods

        public void InitializePagination(DataTable data, int itemsPerPage = 6)
        {
            if (data == null) return;

            dataSource = data;
            pageSize = itemsPerPage;
            totalRecords = data.Rows.Count;
            totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            currentPage = 1;

            UpdatePaginationDisplay();
        }

        public DataTable GetCurrentPageData()
        {
            if (dataSource == null || dataSource.Rows.Count == 0)
                return new DataTable();

            var currentPageData = dataSource.Clone();
            int startIndex = (currentPage - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize, totalRecords);

            for (int i = startIndex; i < endIndex; i++)
            {
                currentPageData.ImportRow(dataSource.Rows[i]);
            }

            return currentPageData;
        }

        public void ForceShow()
        {
            this.Visible = true;
            UpdatePaginationDisplay();
        }

        public void SetPageSize(int size)
        {
            pageSize = size;
            if (dataSource != null)
            {
                totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
                currentPage = Math.Min(currentPage, totalPages);
                UpdatePaginationDisplay();
            }
        }

        #endregion

        #region Private Methods

        private void UpdatePaginationDisplay()
        {
            if (totalRecords == 0)
            {
                PaginationPageNumber.Text = "No records found";
                GoleftButton.Enabled = false;
                GoRightButton.Enabled = false;
                return;
            }

            // Calculate the range of records being shown
            int startRecord = ((currentPage - 1) * pageSize) + 1;
            int endRecord = Math.Min(currentPage * pageSize, totalRecords);

            PaginationPageNumber.Text = $"Page {currentPage} of {totalPages} - Showing {startRecord}-{endRecord} of {totalRecords} records";

            // Enable/disable buttons
            GoleftButton.Enabled = currentPage > 1;
            GoRightButton.Enabled = currentPage < totalPages;

            // Update button styles based on enabled state
            UpdateButtonStyle(GoleftButton, GoleftButton.Enabled);
            UpdateButtonStyle(GoRightButton, GoRightButton.Enabled);
        }

        private void UpdateButtonStyle(Guna.UI2.WinForms.Guna2Button button, bool enabled)
        {
            if (enabled)
            {
                button.FillColor = Color.White;
                button.BorderColor = Color.LightGray;
            }
            else
            {
                button.FillColor = Color.FromArgb(240, 240, 240);
                button.BorderColor = Color.FromArgb(220, 220, 220);
            }
        }

        private int GetCurrentRecordCount()
        {
            if (dataSource == null) return 0;

            int startIndex = (currentPage - 1) * pageSize;
            int remainingRecords = totalRecords - startIndex;
            return Math.Min(pageSize, remainingRecords);
        }

        private void GoToPage(int pageNumber)
        {
            if (pageNumber < 1 || pageNumber > totalPages) return;

            currentPage = pageNumber;
            UpdatePaginationDisplay();
            PageChanged?.Invoke(this, currentPage);
        }

        #endregion

        #region Event Handlers

        private void GoleftButton_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                GoToPage(currentPage - 1);
            }
        }

        private void GoRightButton_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                GoToPage(currentPage + 1);
            }
        }

        private void PaginationPageNumber_Click(object sender, EventArgs e)
        {
            // Optional: Implement page number input dialog
            // ShowPageNumberDialog();
        }

        #endregion

        #region Optional Advanced Features

        // Public properties for external access
        public int CurrentPage => currentPage;
        public int TotalPages => totalPages;
        public int TotalRecords => totalRecords;
        public int PageSize => pageSize;

        #endregion

        private void pagination_panel_Paint(object sender, PaintEventArgs e)
        {
            // Optional: Custom painting for the panel
        }
    }
}