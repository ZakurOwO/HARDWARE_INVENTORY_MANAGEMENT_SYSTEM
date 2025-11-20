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

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module
{
    public partial class InventoryMainPage : UserControl
    {
        private AddItemContainer addItemContainer;
        private SearchTextBox searchTextBox;
        private InventoryList_Table inventoryTable;

        public InventoryMainPage()
        {
            InitializeComponent();
            addItemContainer = new AddItemContainer();
            InitializeSearch();
        }

        private void InitializeSearch()
        {
            TextBox searchBox = FindTextBoxInSearchField(inventory_SearchField1);

            if (searchBox != null)
            {
                // Remove the placeholder text - just pass empty string
                searchTextBox = new SearchTextBox(searchBox, ""); // Empty placeholder
                searchTextBox.SearchTextChanged += SearchTextBox_SearchTextChanged;
            }
            else
            {
                MessageBox.Show("Search textbox not found in the search field.");
            }
        }

        private void SearchTextBox_SearchTextChanged(object sender, string searchText)
        {
            // Find the InventoryList_Table user control
            inventoryTable = FindControlRecursive<InventoryList_Table>(this);

            if (inventoryTable != null)
            {
                // Get the DataGridView from the user control
                DataGridView dgv = FindControlRecursive<DataGridView>(inventoryTable);

                if (dgv != null)
                {
                    searchTextBox.FilterDataGridView(dgv, searchText, 0);
                }
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

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            var main = this.FindForm() as MainDashBoard;
            if (main != null)
            {
                addItemContainer.ShowAddItemForm(main);
            }
        }

        public void CloseAddItemForm()
        {
            addItemContainer.CloseAddItemForm();
        }

        public bool IsAddItemFormVisible()
        {
            return addItemContainer.IsVisible();
        }

        public void ClearSearch()
        {
            searchTextBox?.ClearSearch();
        }

        public void RefreshInventory()
        {
            ClearSearch();
            inventoryTable?.RefreshData();
        }

        private void inventory_SearchField1_Load(object sender, EventArgs e)
        {

        }
    }
}