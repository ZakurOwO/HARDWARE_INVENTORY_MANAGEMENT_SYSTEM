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
        private int totalPages = 1; // Always start with at least 1 page
        private int totalRecords = 0;
        private bool _alwaysShowPagination = true; // New flag

        public int CurrentPage => currentPage;
        public int PageSize => pageSize;
        public int TotalPages => totalPages;
        public int TotalRecords => totalRecords;
        public bool AlwaysShowPagination
        {
            get => _alwaysShowPagination;
            set => _alwaysShowPagination = value;
        }

        public event EventHandler PageChanged;

        public PaginationHelper(DataTable data, int pageSize = 10, bool alwaysShowPagination = true)
        {
            this.originalData = data ?? CreateEmptyDataTable();
            this.pageSize = pageSize;
            this._alwaysShowPagination = alwaysShowPagination;
            CalculateTotalPages();
        }

        private DataTable CreateEmptyDataTable()
        {
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
            totalRecords = originalData?.Rows.Count ?? 0;

            if (pageSize <= 0) pageSize = 10;

            // Always calculate at least 1 page
            totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            if (totalPages < 1) totalPages = 1;
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

        public bool ShouldShowPagination()
        {
            // Always show if flag is true, otherwise only show when multiple pages
            return _alwaysShowPagination || totalPages > 1;
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
            originalData = newData ?? CreateEmptyDataTable();
            CalculateTotalPages();

            if (currentPage > totalPages)
                currentPage = totalPages;

            OnPageChanged();
        }

        public string GetPageInfo()
        {
            if (totalRecords == 0)
                return "Page 1 of 1"; // Always show page info

            int startRecord = ((currentPage - 1) * pageSize) + 1;
            int endRecord = Math.Min(currentPage * pageSize, totalRecords);

            return $"Showing {startRecord}-{endRecord} of {totalRecords} records";
        }

        public string GetSimplePageInfo()
        {
            return $"Page {currentPage} of {totalPages}";
        }

        public List<int> GetPageButtons(int maxButtons = 4)
        {
            var buttons = new List<int>();

            if (totalPages <= maxButtons)
            {
                for (int i = 1; i <= totalPages; i++)
                    buttons.Add(i);
            }
            else
            {
                int startPage = Math.Max(1, currentPage - 1);
                int endPage = Math.Min(totalPages, startPage + maxButtons - 1);

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