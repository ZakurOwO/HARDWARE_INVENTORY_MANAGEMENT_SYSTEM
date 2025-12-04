using System;
using System.Collections.Generic;
using System.Data;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components
{
    public class CustomerPaginationHelper
    {
        private DataTable originalData;
        private int currentPage = 1;
        private int pageSize = 10;
        private int totalPages = 1;
        private int totalRecords = 0;

        public int CurrentPage => currentPage;
        public int PageSize => pageSize;
        public int TotalPages => totalPages;
        public int TotalRecords => totalRecords;

        public event EventHandler PageChanged;

        public CustomerPaginationHelper(DataTable data, int pageSize = 10)
        {
            this.originalData = data ?? CreateEmptyDataTable();
            this.pageSize = pageSize;
            CalculateTotalPages();
        }

        private DataTable CreateEmptyDataTable()
        {
            var emptyTable = new DataTable();
            emptyTable.Columns.Add("customer_name", typeof(string));
            emptyTable.Columns.Add("contact_number", typeof(string));
            emptyTable.Columns.Add("address", typeof(string));
            return emptyTable;
        }

        private void CalculateTotalPages()
        {
            totalRecords = originalData.Rows.Count;
            totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            if (totalPages < 1) totalPages = 1;
        }

        public DataTable GetCurrentPageData()
        {
            if (originalData.Rows.Count == 0)
                return originalData.Clone();

            DataTable pageData = originalData.Clone();
            int startIndex = (currentPage - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize - 1, totalRecords - 1);

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

        public void UpdateData(DataTable newData, bool resetToFirstPage = false)
        {
            originalData = newData ?? CreateEmptyDataTable();
            CalculateTotalPages();

            if (resetToFirstPage)
            {
                currentPage = 1;
            }
            else if (currentPage > totalPages)
            {
                currentPage = totalPages;
            }

            OnPageChanged();
        }

        public string GetPageInfo()
        {
            if (totalRecords == 0)
                return "No records found";

            int startRecord = ((currentPage - 1) * pageSize) + 1;
            int endRecord = Math.Min(currentPage * pageSize, totalRecords);

            return $"Showing {startRecord}-{endRecord} of {totalRecords} customers";
        }

        protected virtual void OnPageChanged()
        {
            PageChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}