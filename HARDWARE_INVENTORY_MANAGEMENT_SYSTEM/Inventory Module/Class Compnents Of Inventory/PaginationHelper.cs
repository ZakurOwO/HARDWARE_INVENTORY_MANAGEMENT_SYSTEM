using System;
using System.Collections.Generic;
using System.Data;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components
{
    public class PaginationHelper
    {
        private DataTable originalData;
        private int currentPage = 1;
        private int pageSize = 10;
        private int totalPages = 0;
        private int totalRecords = 0;

        public int CurrentPage => currentPage;
        public int PageSize => pageSize;
        public int TotalPages => totalPages;
        public int TotalRecords => totalRecords;

        public event EventHandler PageChanged;

        public PaginationHelper(DataTable data, int pageSize = 10)
        {
            // Initialize with empty DataTable if null
            this.originalData = data ?? CreateEmptyDataTable();
            this.pageSize = pageSize;
            CalculateTotalPages();
        }

        private DataTable CreateEmptyDataTable()
        {
            // Create an empty DataTable with the expected structure
            var emptyTable = new DataTable();
            emptyTable.Columns.Add("product_name", typeof(string));
            emptyTable.Columns.Add("SKU", typeof(string));
            emptyTable.Columns.Add("category_name", typeof(string));
            emptyTable.Columns.Add("current_stock", typeof(int));
            emptyTable.Columns.Add("reorder_point", typeof(int));
            emptyTable.Columns.Add("status", typeof(string));
            emptyTable.Columns.Add("image_path", typeof(string));
            emptyTable.Columns.Add("brand", typeof(string));
            return emptyTable;
        }

        private void CalculateTotalPages()
        {
            // Safe null checking
            if (originalData == null)
            {
                totalRecords = 0;
                totalPages = 1;
                return;
            }

            totalRecords = originalData.Rows.Count;
            totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            if (totalPages == 0) totalPages = 1;
        }

        public DataTable GetCurrentPageData()
        {
            if (originalData == null || originalData.Rows.Count == 0)
                return originalData?.Clone() ?? CreateEmptyDataTable();

            int startIndex = (currentPage - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize - 1, totalRecords - 1);

            DataTable pageData = originalData.Clone();
            for (int i = startIndex; i <= endIndex; i++)
            {
                pageData.ImportRow(originalData.Rows[i]);
            }

            return pageData;
        }

        public bool GoToPage(int pageNumber)
        {
            if (pageNumber < 1 || pageNumber > totalPages)
                return false;

            currentPage = pageNumber;
            OnPageChanged();
            return true;
        }

        public bool NextPage()
        {
            if (currentPage >= totalPages)
                return false;

            currentPage++;
            OnPageChanged();
            return true;
        }

        public bool PreviousPage()
        {
            if (currentPage <= 1)
                return false;

            currentPage--;
            OnPageChanged();
            return true;
        }

        public void FirstPage()
        {
            if (currentPage != 1)
            {
                currentPage = 1;
                OnPageChanged();
            }
        }

        public void LastPage()
        {
            if (currentPage != totalPages)
            {
                currentPage = totalPages;
                OnPageChanged();
            }
        }

        public void UpdateData(DataTable newData)
        {
            // Use empty table if null
            originalData = newData ?? CreateEmptyDataTable();
            CalculateTotalPages();

            // Reset to first page if current page is beyond new total
            if (currentPage > totalPages)
                currentPage = totalPages > 0 ? totalPages : 1;

            OnPageChanged();
        }

        public string GetPageInfo()
        {
            if (totalRecords == 0)
                return "Showing 0 out of 0 records";

            int startRecord = ((currentPage - 1) * pageSize) + 1;
            int endRecord = Math.Min(currentPage * pageSize, totalRecords);

            return $"Showing {startRecord}-{endRecord} out of {totalRecords} records";
        }

        public List<int> GetPageButtons(int maxButtons = 4)
        {
            var buttons = new List<int>();

            if (totalPages <= maxButtons)
            {
                // Show all pages if total pages is less than or equal to max buttons
                for (int i = 1; i <= totalPages; i++)
                    buttons.Add(i);
            }
            else
            {
                // Show sliding window of page buttons
                int startPage = Math.Max(1, currentPage - 1);
                int endPage = Math.Min(totalPages, startPage + maxButtons - 1);

                // Adjust if we're at the end
                if (endPage - startPage + 1 < maxButtons)
                {
                    startPage = Math.Max(1, endPage - maxButtons + 1);
                }

                for (int i = startPage; i <= endPage; i++)
                {
                    buttons.Add(i);
                }
            }

            return buttons;
        }

        protected virtual void OnPageChanged()
        {
            PageChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}