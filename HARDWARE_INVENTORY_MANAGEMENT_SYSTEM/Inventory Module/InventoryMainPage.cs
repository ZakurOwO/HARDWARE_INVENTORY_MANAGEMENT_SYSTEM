
using System;
using System.Drawing;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module;

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
                inventoryTable = FindControlRecursive<InventoryList_Table>(this);

                if (inventoryTable != null && inventory_Pagination1 != null)
                {
                    inventoryTable.PaginationControl = inventory_Pagination1;
                    inventory_Pagination1.AlwaysShowPagination = true;
                    inventory_Pagination1.Visible = true;
                    inventory_Pagination1.BringToFront();
                }
            }
            catch
            {
                // Keep page usable even if pagination hookup fails
            }
        }

        private void InitializeItemDescriptionForm()
        {
            itemDescriptionForm = new ItemDescription_Form();
            itemDescriptionForm.Visible = false;
            itemDescriptionForm.CloseRequested += ItemDescriptionForm_CloseRequested;
            Controls.Add(itemDescriptionForm);
            itemDescriptionForm.BringToFront();
        }

        private void ItemDescriptionForm_CloseRequested(object sender, EventArgs e)
        {
            HideItemDescription();
        }

        private void InitializeSearch()
        {
            TextBox searchBox = FindControlRecursive<TextBox>(inventory_SearchField1);

            if (searchBox != null)
            {
                searchTextBox = new SearchTextBox(searchBox, string.Empty);
                searchTextBox.SearchTextChanged += SearchTextBox_SearchTextChanged;
            }
        }

        private void SearchTextBox_SearchTextChanged(object sender, string searchText)
        {
            if (inventoryTable == null)
            {
                inventoryTable = FindControlRecursive<InventoryList_Table>(this);
            }

            if (inventoryTable == null)
            {
                return;
            }

            DataGridView dgv = FindControlRecursive<DataGridView>(inventoryTable);
            if (dgv == null)
            {
                return;
            }

            ApplyInventorySearchFilter(dgv, searchText);
        }

        private void ApplyInventorySearchFilter(DataGridView dgv, string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        row.Visible = true;
                    }
                }
                return;
            }

            string criteria = searchText.Trim();
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }

                string productName = row.Cells[0].Value != null ? row.Cells[0].Value.ToString() : string.Empty;
                string sku = string.Empty;
                if (dgv.Columns.Contains("SKU") && row.Cells["SKU"].Value != null)
                {
                    sku = row.Cells["SKU"].Value.ToString();
                }
                else if (row.Cells.Count > 1 && row.Cells[1].Value != null)
                {
                    sku = row.Cells[1].Value.ToString(); // fallback if your SKU is column index 1
                }

                bool matchName = productName.IndexOf(criteria, StringComparison.OrdinalIgnoreCase) >= 0;
                bool matchSku = sku.IndexOf(criteria, StringComparison.OrdinalIgnoreCase) >= 0;
                row.Visible = matchName || matchSku;
            }
        }

        public void ShowAdjustStockForProduct(string productId, string productName, string sku, string brand, int stock, string imagePath)
        {
            if (adjustStockManager == null)
            {
                adjustStockManager = new AdjustStockManager(this);
            }

            adjustStockManager.ShowAdjustStockPopup(productId, productName, sku, brand, stock, imagePath, RefreshInventory);
        }

        public void ShowItemDescriptionForProduct(string productId, string productName, string sku, string category, int currentStock, string brand, string imagePath)
        {
            if (itemDescriptionForm == null)
            {
                InitializeItemDescriptionForm();
            }

            itemDescriptionForm.Location = new Point(
                (Width - itemDescriptionForm.Width) / 2,
                (Height - itemDescriptionForm.Height) / 2
            );

            var productDetails = InventoryDatabaseHelper.GetProductDetails(productId);
            var timelineDates = InventoryDatabaseHelper.GetRecentActivityDates(productId, sku, productName);

            if (productDetails != null)
            {
                itemDescriptionForm.DisplayProductDetails(productDetails, timelineDates);
            }
            else
            {
                itemDescriptionForm.PopulateProductData(
                    productId,
                    productName,
                    sku,
                    category,
                    currentStock,
                    0.00m,
                    "Available",
                    brand,
                    0,
                    0.00m,
                    "Piece",
                    brand,
                    imagePath);
            }

            itemDescriptionForm.ShowItemDescription();
        }

        public void HideItemDescription()
        {
            if (itemDescriptionForm != null)
            {
                itemDescriptionForm.HideItemDescription();
            }
        }

        private T FindControlRecursive<T>(Control parent) where T : Control
        {
            if (parent == null)
            {
                return null;
            }

            foreach (Control control in parent.Controls)
            {
                T typedControl = control as T;
                if (typedControl != null)
                {
                    return typedControl;
                }

                T child = FindControlRecursive<T>(control);
                if (child != null)
                {
                    return child;
                }
            }

            return null;
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            MainDashBoard main = FindForm() as MainDashBoard;
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
            if (searchTextBox != null)
            {
                searchTextBox.ClearSearch();
            }
        }

        public void RefreshInventory()
        {
            ClearSearch();
            if (inventoryTable != null)
            {
                inventoryTable.RefreshData();
            }

            if (inventory_Pagination1 != null)
            {
                inventory_Pagination1.ForceShow();
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (Visible && inventory_Pagination1 != null)
            {
                inventory_Pagination1.ForceShow();
            }
        }

        private void inventory_SearchField1_Load(object sender, EventArgs e)
        {
            InitializeSearch();
        }

        private void inventory_Pagination1_Load(object sender, EventArgs e)
        {
            if (inventory_Pagination1 != null)
            {
                inventory_Pagination1.ForceShow();
            }
        }

        public void ShowEditItemForm(string productId, PictureBox overlay)
        {
            EditItem_Form edit = new EditItem_Form(productId, overlay);
            edit.OnProductUpdated += (s, e) => RefreshInventory();

            // Add to page
            edit.Dock = DockStyle.None;
            edit.Location = new Point(
                (this.Width - edit.Width) / 2,
                (this.Height - edit.Height) / 2
            );

            this.Controls.Add(edit);
            edit.BringToFront();
        }
    }
}