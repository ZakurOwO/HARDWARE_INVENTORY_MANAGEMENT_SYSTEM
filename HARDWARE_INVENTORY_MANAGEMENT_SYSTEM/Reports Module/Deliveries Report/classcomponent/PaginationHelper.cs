using System;
using System.Data;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components
{
    public class PaginationHelper
    {
        private DataTable data;
        private int pageSize;
        private int currentPage = 1;
        private int totalPages = 1;

        public event EventHandler PageChanged;

        public int CurrentPage => currentPage;
        public int TotalPages => totalPages;
        public int PageSize => pageSize;

        public PaginationHelper(DataTable data, int pageSize)
        {
            this.data = data;
            this.pageSize = pageSize;
            CalculateTotalPages();
        }

        public void UpdateData(DataTable newData)
        {
            this.data = newData;
            CalculateTotalPages();
            currentPage = 1;
            OnPageChanged();
        }

        private void CalculateTotalPages()
        {
            if (data == null || pageSize <= 0)
            {
                totalPages = 1;
                return;
            }

            totalPages = (int)Math.Ceiling((double)data.Rows.Count / pageSize);
            if (totalPages == 0) totalPages = 1;
        }

        public DataTable GetCurrentPageData()
        {
            if (data == null || data.Rows.Count == 0)
                return data;

            DataTable pageData = data.Clone();
            int startIndex = (currentPage - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize, data.Rows.Count);

            for (int i = startIndex; i < endIndex; i++)
            {
                pageData.ImportRow(data.Rows[i]);
            }

            return pageData;
        }

        public void FirstPage()
        {
            if (currentPage > 1)
            {
                currentPage = 1;
                OnPageChanged();
            }
        }

        public void PreviousPage()
        {
            if (currentPage > 1)
            {
                currentPage--;
                OnPageChanged();
            }
        }

        public void NextPage()
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                OnPageChanged();
            }
        }

        public void LastPage()
        {
            if (currentPage < totalPages)
            {
                currentPage = totalPages;
                OnPageChanged();
            }
        }

        public string GetPageInfo()
        {
            return $"Page {currentPage} of {totalPages}";
        }

        protected virtual void OnPageChanged()
        {
            PageChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}