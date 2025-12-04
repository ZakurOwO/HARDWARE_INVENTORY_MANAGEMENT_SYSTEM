using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using System;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Customer_Module
{
    public partial class CustomerMainPage : UserControl
    {
        private AddCustomerContainer addCustomerContainer = new AddCustomerContainer();
        private SearchTextBox searchTextBox;
        private Timer searchDelay;

        public CustomerMainPage()
        {
            InitializeComponent();
            this.Load += CustomerMainPage_Load;
        }

        private void CustomerMainPage_Load(object sender, EventArgs e)
        {
            InitializeSearch();
            ConnectPagination();
        }

        private void ConnectPagination()
        {
            try
            {
                // Find the DataGridTable control
                var dataGridTable = FindControlRecursive<DataGridTable>(this);
                var paginationControl = FindControlRecursive<PageNumber>(this);

                if (dataGridTable != null && paginationControl != null)
                {
                    dataGridTable.PaginationControl = paginationControl;

                    // Load initial data
                    dataGridTable.RefreshData();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting pagination: {ex.Message}");
            }
        }

        private void InitializeSearch()
        {
            // Find the search textbox in your searchField1 control
            TextBox searchBox = FindTextBoxInSearchField(searchField1);

            if (searchBox != null)
            {
                searchTextBox = new SearchTextBox(searchBox, "Search customers...");
                searchTextBox.SearchTextChanged += SearchTextBox_SearchTextChanged;
                searchDelay = new Timer { Interval = 200 };
                searchDelay.Tick += SearchDelay_Tick;
            }
        }

        private void SearchTextBox_SearchTextChanged(object sender, string searchText)
        {
            var dataGridTable = FindControlRecursive<DataGridTable>(this);
            if (dataGridTable != null)
            {
                dataGridTable.ApplySearch(searchText);
            }
        }

        private TextBox FindTextBoxInSearchField(Control searchFieldControl)
        {
            return FindControlRecursive<TextBox>(searchFieldControl);
        }

        private T FindControlRecursive<T>(Control parent) where T : Control
        {
            if (parent == null) return null;

            foreach (Control control in parent.Controls)
            {
                if (control is T found)
                    return found;

                var child = FindControlRecursive<T>(control);
                if (child != null)
                    return child;
            }
            return null;
        }

        private void btnMainButtonIcon_Click(object sender, EventArgs e)
        {
            var main = this.FindForm() as MainDashBoard;

            if (main != null)
            {
                addCustomerContainer.ShowAddCustomerForm(main);
                // Refresh customer list after adding new customer
                RefreshCustomerList();
            }
        }

        public void RefreshCustomerList()
        {
            var dataGridTable = FindControlRecursive<DataGridTable>(this);
            string activeSearch = searchTextBox?.GetSearchText();
            dataGridTable?.RefreshData(activeSearch);

            var paginationControl = FindControlRecursive<PageNumber>(this);
            paginationControl?.RefreshPagination();
        }

        // ... rest of your existing event handlers
    }
}