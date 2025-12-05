using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Customer_Module
{
    public partial class CustomerMainPage : UserControl
    {
        private AddCustomerContainer addCustomerContainer = new AddCustomerContainer();
        private TextBox searchBox;

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
                MessageBox.Show($"Error connecting pagination: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeSearch()
        {
            try
            {
                // Find the search textbox
                searchBox = FindTextBoxInSearchField(searchField1);

                if (searchBox == null)
                {
                    Console.WriteLine("Search box not found!");
                    return;
                }

                Console.WriteLine("Search box found and initialized");

                // Clear any existing event handlers to prevent duplicates
                searchBox.TextChanged -= SearchBox_TextChanged;
                searchBox.KeyUp -= SearchBox_KeyUp;

                // Set up placeholder text
                searchBox.ForeColor = Color.Gray;
                searchBox.Text = "Search customers...";

                // Hook up multiple events for comprehensive search
                searchBox.TextChanged += SearchBox_TextChanged;
                searchBox.KeyUp += SearchBox_KeyUp;

                // When user focuses, clear placeholder
                searchBox.GotFocus += (s, e) =>
                {
                    if (searchBox.Text == "Search customers..." && searchBox.ForeColor == Color.Gray)
                    {
                        searchBox.Text = "";
                        searchBox.ForeColor = Color.Black;
                        // Trigger search with empty text
                        PerformSearch("");
                    }
                };

                // When user leaves and text is empty, restore placeholder
                searchBox.LostFocus += (s, e) =>
                {
                    if (string.IsNullOrWhiteSpace(searchBox.Text))
                    {
                        searchBox.Text = "Search customers...";
                        searchBox.ForeColor = Color.Gray;
                        // Reset to show all customers
                        PerformSearch("");
                    }
                };

                Console.WriteLine("Search events attached successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing search: {ex.Message}");
                MessageBox.Show($"Error initializing search: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Ignore if it's the placeholder text
                if (searchBox.Text == "Search customers..." && searchBox.ForeColor == Color.Gray)
                {
                    return;
                }

                string searchText = searchBox.Text.Trim();
                Console.WriteLine($"Search text changed: '{searchText}'");
                PerformSearch(searchText);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SearchBox_TextChanged: {ex.Message}");
            }
        }

        private void SearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                // Ignore if it's the placeholder text
                if (searchBox.Text == "Search customers..." && searchBox.ForeColor == Color.Gray)
                {
                    return;
                }

                string searchText = searchBox.Text.Trim();
                Console.WriteLine($"Key up - Search text: '{searchText}'");
                PerformSearch(searchText);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SearchBox_KeyUp: {ex.Message}");
            }
        }

        private void PerformSearch(string searchText)
        {
            try
            {
                Console.WriteLine($"Performing search with text: '{searchText}'");

                var dataGridTable = FindControlRecursive<DataGridTable>(this);
                var paginationControl = FindControlRecursive<PageNumber>(this);

                if (dataGridTable == null)
                {
                    Console.WriteLine("DataGridTable not found!");
                    return;
                }

                Console.WriteLine("Applying search to DataGridTable...");

                // Apply strict search filter immediately
                dataGridTable.ApplySearch(searchText);

                Console.WriteLine("Search applied successfully");

                // Refresh pagination after search
                if (paginationControl != null)
                {
                    paginationControl.RefreshPagination();
                    Console.WriteLine("Pagination refreshed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error performing search: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                MessageBox.Show($"Search error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private TextBox FindTextBoxInSearchField(Control searchFieldControl)
        {
            if (searchFieldControl == null)
            {
                Console.WriteLine("searchField1 is null!");
                return null;
            }

            return FindControlRecursive<TextBox>(searchFieldControl);
        }

        private T FindControlRecursive<T>(Control parent) where T : Control
        {
            if (parent == null) return null;

            // Check if parent itself is T
            if (parent is T found)
                return found;

            foreach (Control control in parent.Controls)
            {
                if (control is T result)
                    return result;

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
            try
            {
                var dataGridTable = FindControlRecursive<DataGridTable>(this);
                var paginationControl = FindControlRecursive<PageNumber>(this);

                if (dataGridTable == null)
                {
                    Console.WriteLine("DataGridTable not found in RefreshCustomerList");
                    return;
                }

                // Get current search text
                string activeSearch = string.Empty;
                if (searchBox != null &&
                    searchBox.Text != "Search customers..." &&
                    searchBox.ForeColor != Color.Gray)
                {
                    activeSearch = searchBox.Text.Trim();
                }

                Console.WriteLine($"Refreshing with search: '{activeSearch}'");

                dataGridTable.ApplySearch(activeSearch);
                paginationControl?.RefreshPagination();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in RefreshCustomerList: {ex.Message}");
                MessageBox.Show($"Error refreshing customer list: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void refreshbtnn_Click(object sender, EventArgs e)
        {
            // Clear search and refresh
            if (searchBox != null)
            {
                searchBox.Text = "Search customers...";
                searchBox.ForeColor = Color.Gray;
            }

            RefreshCustomerList();
        }

        private void dataGridTable1_Load(object sender, EventArgs e)
        {

        }
    }
}