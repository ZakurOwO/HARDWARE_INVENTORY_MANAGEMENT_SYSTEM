using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Services;
using ScottPlot.Colormaps;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

// IMPORTANT: This must match the namespace where EditItemContainer really lives
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components.Class_Compnents_Of_Inventory;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module
{
    public partial class InventoryList_Table : UserControl
    {

        private EditItemContainer _editItemContainer;

        // MOVEABLE: tweak these if you want it “a little left” later
        // Example: set _editOverlayOffsetX = -25; to move left 25px
        private int _editOverlayOffsetX = 0;
        private int _editOverlayOffsetY = 0;

        private PaginationHelperCustomer paginationHelper;
        private DataTable allProductsData;
        public Inventory_Pagination PaginationControl { get; set; }

        public InventoryList_Table()
        {
            InitializeComponent();
        }

        private void InventoryList_Table_Load(object sender, EventArgs e)
        {
            LoadDataFromDatabase();

            // Make sure this line is here and not commented out!
            dgvInventoryList.CellClick += dgvInventoryList_CellClick;

            dgvInventoryList.DataError += dgvInventoryList_DataError;
            dgvInventoryList.ClearSelection();
        }

        private void dgvInventoryList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvInventoryList_CellClick(sender, e);
        }

        private void dgvInventoryList_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Prevent default error dialogs when the image column receives non-image values
            if (e.ColumnIndex >= 0 && dgvInventoryList.Columns[e.ColumnIndex].Name == "ProductImage")
            {
                e.ThrowException = false;
            }
        }

        public void RefreshData()
        {
            LoadDataFromDatabase();
        }

        private void LoadDataFromDatabase()
        {
            try
            {
                // Load all data into DataTable first
                allProductsData = new DataTable();

                using (SqlConnection connection = new SqlConnection(ConnectionString.DataSource))
                {
                    connection.Open();
                    bool hasImageColumn = InventoryDatabaseHelper.ProductImageColumnExists();
                    string query = hasImageColumn
                        ? @"
                        SELECT
                            p.ProductInternalID,
                            p.ProductID,
                            p.product_name,
                            p.SKU,
                            c.category_name,
                            p.current_stock,
                            p.reorder_point,
                            CASE WHEN p.active = 1 THEN 'Active' ELSE 'Inactive' END as status,
                            p.image_path,
                            p.product_image,
                            p.description as brand
                        FROM Products p
                        INNER JOIN Categories c ON p.category_id = c.CategoryID
                        ORDER BY p.product_name"
                        : @"
                        SELECT
                            p.ProductInternalID,
                            p.ProductID,
                            p.product_name,
                            p.SKU,
                            c.category_name,
                            p.current_stock,
                            p.reorder_point,
                            CASE WHEN p.active = 1 THEN 'Active' ELSE 'Inactive' END as status,
                            p.image_path,
                            p.description as brand
                        FROM Products p
                        INNER JOIN Categories c ON p.category_id = c.CategoryID
                        ORDER BY p.product_name";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(allProductsData);
                        }
                    }
                }

                // Initialize or update pagination
                InitializePagination();
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"SQL Error loading data: {sqlEx.Message}", "Database Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializePagination()
        {
            // If we have a pagination control, use it
            if (PaginationControl != null)
            {
                PaginationControl.AlwaysShowPagination = true;
                PaginationControl.InitializePagination(allProductsData, dgvInventoryList, 10);
                PaginationControl.PageChanged += (s, pageNumber) =>
                {
                    if (paginationHelper != null)
                    {
                        paginationHelper.GoToPage(pageNumber);
                    }
                };
                PaginationControl.ForceShow();
            }
            else
            {
                // Create internal pagination helper as fallback
                paginationHelper = new PaginationHelperCustomer(allProductsData, 10, true);
                paginationHelper.PageChanged += PaginationHelper_PageChanged;
            }

            // Load the first page
            LoadPageData();
        }

        private void PaginationHelper_PageChanged(object sender, EventArgs e)
        {
            LoadPageData();
        }

        private void LoadPageData()
        {
            try
            {
                dgvInventoryList.Rows.Clear();
                ProductGridImageBinder.ClearCache();

                DataTable pageData;

                if (PaginationControl != null && paginationHelper != null)
                {
                    pageData = paginationHelper.GetCurrentPageData();
                }
                else if (paginationHelper != null)
                {
                    pageData = paginationHelper.GetCurrentPageData();
                }
                else
                {
                    // No pagination, show all data
                    pageData = allProductsData;
                }

                var rowModels = new BindingList<ProductGridImageBinder.ProductRowModel>();

                foreach (DataRow row in pageData.Rows)
                {
                    int.TryParse(row["ProductInternalID"].ToString(), out int productInternalId);
                    byte[] productImage = null;
                    if (pageData.Columns.Contains("product_image") && row["product_image"] != DBNull.Value)
                    {
                        productImage = (byte[])row["product_image"];
                    }

                    rowModels.Add(new ProductGridImageBinder.ProductRowModel
                    {
                        ProductInternalId = productInternalId,
                        ProductId = row["ProductID"].ToString(),
                        ProductName = row["product_name"].ToString(),
                        SKU = row["SKU"].ToString(),
                        Category = row["category_name"].ToString(),
                        CurrentStock = Convert.ToInt32(row["current_stock"]),
                        ReorderPoint = Convert.ToInt32(row["reorder_point"]),
                        Status = row["status"].ToString(),
                        ImagePath = row["image_path"].ToString(),
                        ProductImage = productImage,
                        Brand = row["brand"].ToString()
                    });
                }

                ProductGridImageBinder.BindRows(dgvInventoryList, rowModels);

                // Update pagination display
                PaginationControl?.RefreshPagination();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading page data: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvInventoryList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvInventoryList.Rows.Count) return;
            if (e.ColumnIndex < 0 || e.ColumnIndex >= dgvInventoryList.Columns.Count) return;

            string columnName = dgvInventoryList.Columns[e.ColumnIndex].Name;

            // Adjust Stock button column
            if (columnName == "AdjustStock" || columnName == "btnSettings")
            {
                try
                {
                    string productName = dgvInventoryList.Rows[e.RowIndex].Cells[0].Value?.ToString() ?? "";
                    string category = dgvInventoryList.Rows[e.RowIndex].Cells[2].Value?.ToString() ?? "";

                    int currentStock = 0;
                    if (dgvInventoryList.Rows[e.RowIndex].Cells[3].Value != null)
                    {
                        int.TryParse(dgvInventoryList.Rows[e.RowIndex].Cells[3].Value.ToString(), out currentStock);
                    }

                    string imagePath = "";
                    string sku = "";
                    string brand = "";
                    string productId = "";
                    int productInternalId = 0;

                    if (dgvInventoryList.Rows[e.RowIndex].Tag is ProductGridImageBinder.ProductRowModel rowData)
                    {
                        productInternalId = rowData.ProductInternalId;
                        imagePath = rowData.ImagePath ?? "";
                        sku = rowData.SKU ?? "";
                        brand = rowData.Brand ?? "";
                        productId = rowData.ProductId ?? "";
                    }

                    var mainPage = FindParentOfType<InventoryMainPage>(this);
                    if (mainPage != null && productInternalId > 0 && !string.IsNullOrEmpty(productName))
                    {
                        mainPage.ShowAdjustStockForProduct(productId, productName, sku, brand, currentStock, imagePath);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening adjust stock: {ex.Message}");
                }
            }
            else if (columnName == "Deactivate")
            {
                try
                {
                    string productName = dgvInventoryList.Rows[e.RowIndex].Cells[0].Value?.ToString() ?? "";
                    string currentStatus = dgvInventoryList.Rows[e.RowIndex].Cells[5].Value?.ToString() ?? "";

                    if (string.IsNullOrEmpty(productName))
                    {
                        MessageBox.Show("No product selected.", "Error",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    bool newActiveStatus;
                    string actionText;
                    string confirmationMessage;

                    if (currentStatus == "Active")
                    {
                        newActiveStatus = false;
                        actionText = "deactivate";
                        confirmationMessage = $"Are you sure you want to deactivate '{productName}'?\n\nDeactivated products will not appear in sales and reports.";
                    }
                    else
                    {
                        newActiveStatus = true;
                        actionText = "activate";
                        confirmationMessage = $"Are you sure you want to activate '{productName}'?";
                    }

                    DialogResult result = MessageBox.Show(confirmationMessage,
                        $"Confirm {actionText}",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        bool success = UpdateProductStatus(productName, newActiveStatus);

                        if (success)
                        {
                            MessageBox.Show($"Product '{productName}' has been {actionText}d successfully!",
                                          "Success",
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Information);
                            RefreshData();
                        }
                        else
                        {
                            MessageBox.Show($"Failed to {actionText} product '{productName}'. Please try again.",
                                          "Error",
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating product status: {ex.Message}",
                                  "Error",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Error);
                }
            }
            else if (columnName == "ViewDetails")
            {
                try
                {
                    string productName = dgvInventoryList.Rows[e.RowIndex].Cells[0].Value?.ToString() ?? "";
                    string category = dgvInventoryList.Rows[e.RowIndex].Cells[2].Value?.ToString() ?? "";

                    int currentStock = 0;
                    if (dgvInventoryList.Rows[e.RowIndex].Cells[3].Value != null)
                    {
                        int.TryParse(dgvInventoryList.Rows[e.RowIndex].Cells[3].Value.ToString(), out currentStock);
                    }

                    string imagePath = "";
                    string sku = "";
                    string brand = "";
                    string productId = "";

                    if (dgvInventoryList.Rows[e.RowIndex].Tag is ProductGridImageBinder.ProductRowModel rowData)
                    {
                        imagePath = rowData.ImagePath ?? "";
                        sku = rowData.SKU ?? "";
                        brand = rowData.Brand ?? "";
                        productId = rowData.ProductId ?? "";
                    }

                    var mainPage = FindParentOfType<InventoryMainPage>(this);
                    if (mainPage != null && !string.IsNullOrEmpty(productName))
                    {
                        mainPage.ShowItemDescriptionForProduct(productId, productName, sku, category, currentStock, brand, imagePath);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening item description: {ex.Message}");
                }
            }
            else if (columnName == "Edit")
            {
                try
                {
                    if (dgvInventoryList.Rows[e.RowIndex].Tag is ProductGridImageBinder.ProductRowModel rowData)
                    {
                        string productId = rowData.ProductId;

                        var mainPage = FindParentOfType<InventoryMainPage>(this);
                        if (mainPage == null) return;

                        var main = mainPage.FindForm() as MainDashBoard;
                        if (main == null) return;

                        // Create container once (re-use it)
                        if (_editItemContainer == null)
                            _editItemContainer = new EditItemContainer();

                        // Show the edit form using your container
                        _editItemContainer.ShowEditItemForm(main, productId);

                        // MOVEABLE: adjust placement AFTER it is shown
                        // This is safe because the container exposes the panel reference.
                        Panel p = _editItemContainer.ActiveContainerPanel;
                        if (p != null)
                        {
                            p.Location = new Point(p.Location.X + _editOverlayOffsetX, p.Location.Y + _editOverlayOffsetY);
                            p.BringToFront();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening edit item form: {ex.Message}");
                }
            }
        }

        private bool UpdateProductStatus(string productName, bool isActive)
        {
            using (var connection = new SqlConnection(ConnectionString.DataSource))
            {
                connection.Open();
                string query = "UPDATE Products SET active = @isActive WHERE product_name = @productName";

                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@isActive", isActive);
                    cmd.Parameters.AddWithValue("@productName", productName);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        // Helper method to find parent of specific type
        private T FindParentOfType<T>(Control control) where T : Control
        {
            Control parent = control.Parent;
            while (parent != null)
            {
                if (parent is T found)
                    return found;
                parent = parent.Parent;
            }
            return null;
        }

        // Public method to refresh pagination when data changes
        public void RefreshPagination()
        {
            LoadDataFromDatabase();
        }
    }
}
