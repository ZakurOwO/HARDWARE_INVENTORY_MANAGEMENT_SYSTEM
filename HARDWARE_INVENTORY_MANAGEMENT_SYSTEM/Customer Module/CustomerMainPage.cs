using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using System;
using System.Drawing;
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
                var dataGridTable = FindControlRecursive<DataGridTable>(this);
                var paginationControl = FindControlRecursive<PageNumber>(this);

                if (dataGridTable != null && paginationControl != null)
                {
                    dataGridTable.PaginationControl = paginationControl;

                    // Initial load (no search filter)
                    dataGridTable.RefreshData();
                    paginationControl.RefreshPagination();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting pagination: {ex.Message}");
            }
        }

        private void InitializeSearch()
        {
            TextBox searchBox = FindTextBoxInSearchField(searchField1);

            if (searchBox != null)
            {
                searchBox.ForeColor = Color.Gray;
                searchBox.Text = "Search customers...";

                // When user focuses, clear placeholder
                searchBox.GotFocus += (s, e) =>
                {
                    if (searchBox.Text == "Search customers...")
                    {
                        searchBox.Text = "";
                        searchBox.ForeColor = Color.Black;
                    }
                };

                // When user leaves and text is empty, restore placeholder
                searchBox.LostFocus += (s, e) =>
                {
                    if (string.IsNullOrWhiteSpace(searchBox.Text))
                    {
                        searchBox.Text = "Search customers...";
                        searchBox.ForeColor = Color.Gray;
                    }
                };
            }
        }


        private void SearchTextBox_SearchTextChanged(object sender, string searchText)
        {
            if (searchDelay == null) return;

            // debounce: restart timer on each keystroke
            searchDelay.Stop();
            searchDelay.Start();
        }

        private void SearchDelay_Tick(object sender, EventArgs e)
        {
            searchDelay.Stop();

            string searchText = searchTextBox?.GetSearchText() ?? string.Empty;

            var dataGridTable = FindControlRecursive<DataGridTable>(this);
            var paginationControl = FindControlRecursive<PageNumber>(this);

            if (dataGridTable != null)
            {
                // DataGridTable already knows how to apply search text
                dataGridTable.ApplySearch(searchText);
            }

            paginationControl?.RefreshPagination();
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
                // Container handles blur + overlay + refresh events
                addCustomerContainer.ShowAddCustomerForm(main);
            }
        }

        /// <summary>
        /// Called after add/edit/delete to reload the grid and keep the current search filter.
        /// </summary>
        public void RefreshCustomerList()
        {
            var dataGridTable = FindControlRecursive<DataGridTable>(this);
            var paginationControl = FindControlRecursive<PageNumber>(this);

            if (dataGridTable == null) return;

            string activeSearch = searchTextBox?.GetSearchText() ?? string.Empty;

            dataGridTable.ApplySearch(activeSearch);
            paginationControl?.RefreshPagination();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void refreshbtnn_Click(object sender, EventArgs e)
        {

        }

        private void dataGridTable1_Load(object sender, EventArgs e)
        {

        }
    }
}
