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
        private AdjustStockManager adjustStockManager;
        private ItemDescription_Form itemDescriptionForm;

        public InventoryMainPage()
        {
            InitializeComponent();
            addItemContainer = new AddItemContainer();
            adjustStockManager = new AdjustStockManager(this);
            InitializeSearch();
            InitializeItemDescriptionForm();
            ConnectPagination();
        }

        private void ConnectPagination()
        {
            try
            {
                // Find the inventory table control
                inventoryTable = FindControlRecursive<InventoryList_Table>(this);

                if (inventoryTable != null && inventory_Pagination1 != null)
                {
                    // Set the pagination control reference
                    inventoryTable.PaginationControl = inventory_Pagination1;

                    // Force pagination to always show
                    inventory_Pagination1.AlwaysShowPagination = true;
                    inventory_Pagination1.Visible = true;
                    inventory_Pagination1.BringToFront();

                    Console.WriteLine("Pagination connected successfully!");
                }
                else
                {
                    Console.WriteLine("Pagination controls not found!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting pagination: {ex.Message}");
            }
        }

        private void InitializeItemDescriptionForm()
        {
            itemDescriptionForm = new ItemDescription_Form();
            itemDescriptionForm.Visible = false;
            itemDescriptionForm.CloseRequested += (s, e) => HideItemDescription();
            this.Controls.Add(itemDescriptionForm);
            itemDescriptionForm.BringToFront();
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

        // Method to call when user clicks adjust stock in DataGridView
        public void ShowAdjustStockForProduct(string productName, string sku, string brand, int stock, string imagePath)
        {
            adjustStockManager.ShowAdjustStockPopup(productName, sku, brand, stock, imagePath, RefreshInventory);
        }

        // Method to call when user clicks view details in DataGridView
        public void ShowItemDescriptionForProduct(string productName, string sku, string category, int currentStock, string brand, string imagePath)
        {
            if (itemDescriptionForm == null)
                InitializeItemDescriptionForm();

            // Center the form in the main page
            itemDescriptionForm.Location = new Point(
                (this.Width - itemDescriptionForm.Width) / 2,
                (this.Height - itemDescriptionForm.Height) / 2
            );

            // Use the simplified version with image path
            itemDescriptionForm.PopulateProductData(
                productName: productName,
                sku: sku,
                category: category,
                currentStock: currentStock,
                sellingPrice: 0.00m, // You'll need to get this from database
                status: "Available", // You'll need to get this from database
                brand: brand,
                minimumStock: 0, // You'll need to get this from database
                costPrice: 0.00m, // You'll need to get this from database
                unit: "Piece", // You'll need to get this from database
                description: brand, // Using brand as description for now
                imagePath: imagePath // Add the image path
            );

            itemDescriptionForm.ShowItemDescription();
        }

        public void HideItemDescription()
        {
            itemDescriptionForm?.HideItemDescription();
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

            // Ensure pagination is visible after refresh
            inventory_Pagination1?.ForceShow();
        }

        // ADD THIS METHOD to ensure pagination stays visible
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (this.Visible && inventory_Pagination1 != null)
            {
                inventory_Pagination1.ForceShow();
            }
        }

        private void inventory_SearchField1_Load(object sender, EventArgs e)
        {
            // Re-initialize search when search field loads
            InitializeSearch();
        }

        private void inventory_Pagination1_Load(object sender, EventArgs e)
        {
            // Ensure pagination is visible when it loads
            inventory_Pagination1?.ForceShow();
        }
    }
}