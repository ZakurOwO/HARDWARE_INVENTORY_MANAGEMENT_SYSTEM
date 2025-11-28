using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Deliveries
{
    public partial class Pagination_Deliveries : UserControl
    {
        // Events
        public event EventHandler<int> PageChanged;

        // Properties
        private int currentPage = 1;
        private int totalPages = 1;
        private int pageSize = 6;
        private int totalRecords = 0;
        private DataTable dataSource;

        public Pagination_Deliveries()
        {
            InitializeComponent();
            FixButtonImages();
            UpdatePaginationDisplay();

            DebugMessage("Pagination_Deliveries constructor completed");
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
            DebugMessage("=== Pagination.InitializePagination Called ===");

            if (data == null)
            {
                DebugMessage("ERROR: Data is NULL!");
                return;
            }

            DebugMessage($"Data received: {data.Rows.Count} rows");
            DebugMessage($"Items per page: {itemsPerPage}");

            dataSource = data;
            pageSize = itemsPerPage;
            totalRecords = data.Rows.Count;
            totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            currentPage = 1;

            DebugMessage($"Calculated - TotalRecords: {totalRecords}, TotalPages: {totalPages}, PageSize: {pageSize}");

            UpdatePaginationDisplay();

            DebugMessage("=== Pagination.InitializePagination Completed ===");
        }

        public DataTable GetCurrentPageData()
        {
            DebugMessage($"GetCurrentPageData called - Page {currentPage} of {totalPages}");

            if (dataSource == null || dataSource.Rows.Count == 0)
            {
                DebugMessage("WARNING: dataSource is null or empty");
                return new DataTable();
            }

            var currentPageData = dataSource.Clone();
            int startIndex = (currentPage - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize, totalRecords);

            DebugMessage($"Getting rows {startIndex} to {endIndex - 1} (total: {endIndex - startIndex} rows)");

            for (int i = startIndex; i < endIndex; i++)
            {
                currentPageData.ImportRow(dataSource.Rows[i]);
            }

            DebugMessage($"Returning {currentPageData.Rows.Count} rows for page {currentPage}");
            return currentPageData;
        }

        public void ForceShow()
        {
            this.Visible = true;
            this.BringToFront();
            UpdatePaginationDisplay();
            DebugMessage($"ForceShow called - Visible: {this.Visible}, Location: {this.Location}");
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

        // Public properties for external access
        public int CurrentPage => currentPage;
        public int TotalPages => totalPages;
        public int TotalRecords => totalRecords;
        public int PageSize => pageSize;

        #endregion

        #region Private Methods

        private void UpdatePaginationDisplay()
        {
            DebugMessage($"UpdatePaginationDisplay called - Page {currentPage} of {totalPages}, Total: {totalRecords}");

            if (totalRecords == 0)
            {
                PaginationPageNumber.Text = "No records found";
                GoleftButton.Enabled = false;
                GoRightButton.Enabled = false;
                DebugMessage("No records - buttons disabled");
                return;
            }

            // Calculate the range of records being shown
            int startRecord = ((currentPage - 1) * pageSize) + 1;
            int endRecord = Math.Min(currentPage * pageSize, totalRecords);

            PaginationPageNumber.Text = $"Page {currentPage} of {totalPages} - Showing {startRecord}-{endRecord} of {totalRecords} records";
            DebugMessage($"Display text: {PaginationPageNumber.Text}");

            // Enable/disable buttons
            GoleftButton.Enabled = currentPage > 1;
            GoRightButton.Enabled = currentPage < totalPages;

            DebugMessage($"Left button enabled: {GoleftButton.Enabled}, Right button enabled: {GoRightButton.Enabled}");

            // Update button styles based on enabled state
            UpdateButtonStyle(GoleftButton, GoleftButton.Enabled);
            UpdateButtonStyle(GoRightButton, GoRightButton.Enabled);
        }

        private void UpdateButtonStyle(Guna2Button button, bool enabled)
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

        private void GoToPage(int pageNumber)
        {
            DebugMessage($"=== GoToPage called with page {pageNumber} ===");

            if (pageNumber < 1 || pageNumber > totalPages)
            {
                DebugMessage($"ERROR: Invalid page number {pageNumber}. Valid range: 1-{totalPages}");
                return;
            }

            currentPage = pageNumber;
            DebugMessage($"Current page set to: {currentPage}");

            UpdatePaginationDisplay();

            DebugMessage($"Raising PageChanged event for page {currentPage}");
            PageChanged?.Invoke(this, currentPage);

            DebugMessage($"=== GoToPage completed ===");
        }

        #endregion

        #region Event Handlers

        private void GoleftButton_Click(object sender, EventArgs e)
        {
            DebugMessage($"Left button clicked - Current page: {currentPage}");
            if (currentPage > 1)
            {
                GoToPage(currentPage - 1);
            }
            else
            {
                DebugMessage("Cannot go left - already on first page");
            }
        }

        private void GoRightButton_Click(object sender, EventArgs e)
        {
            DebugMessage($"Right button clicked - Current page: {currentPage}, Total pages: {totalPages}");
            if (currentPage < totalPages)
            {
                GoToPage(currentPage + 1);
            }
            else
            {
                DebugMessage("Cannot go right - already on last page");
            }
        }

        private void PaginationPageNumber_Click(object sender, EventArgs e)
        {
            // Optional: Implement page number input dialog
            DebugMessage("Page number label clicked");
        }

        #endregion

        #region Debug Methods

        // Debug method
        private void DebugMessage(string message)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss.fff");
            Console.WriteLine($"[{timestamp}] [Pagination] {message}");
        }

        // Public debug method
        public string GetDebugInfo()
        {
            return $"CurrentPage: {currentPage}, TotalPages: {totalPages}, PageSize: {pageSize}, TotalRecords: {totalRecords}, DataSource: {(dataSource != null ? dataSource.Rows.Count + " rows" : "NULL")}";
        }

        #endregion

        private void pagination_panel_Paint(object sender, PaintEventArgs e)
        {
            // Optional: Custom painting for the panel
        }
    }
}