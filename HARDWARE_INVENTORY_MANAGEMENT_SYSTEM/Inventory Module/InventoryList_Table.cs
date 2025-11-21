using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module
{
    public partial class InventoryList_Table : UserControl
    {
        // Simple class to store row data
        public class InventoryRowData
        {
            public string ImagePath { get; set; }
            public string SKU { get; set; }
            public string Brand { get; set; }
        }

        private PaginationHelper paginationHelper;
        private DataTable allProductsData;
        public Inventory_Pagination PaginationControl { get; set; }

        public InventoryList_Table()
        {
            InitializeComponent();
        }

        private void InventoryList_Table_Load(object sender, EventArgs e)
        {
            LoadDataFromDatabase();
            dgvInventoryList.CellClick += dgvInventoryList_CellClick;
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
                    string query = @"
                        SELECT 
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
            // Create pagination helper
            if (paginationHelper == null)
            {
                paginationHelper = new PaginationHelper(allProductsData, 10, true); // Always show
                paginationHelper.PageChanged += PaginationHelper_PageChanged;
            }
            else
            {
                paginationHelper.UpdateData(allProductsData);
            }

            // Initialize pagination control if it exists
            if (PaginationControl != null)
            {
                PaginationControl.AlwaysShowPagination = true; // Force always show
                PaginationControl.InitializePagination(allProductsData, dgvInventoryList, 10);
                PaginationControl.PageChanged += PaginationControl_PageChanged;
                PaginationControl.ForceShow(); // Force visibility
            }
            else
            {
                // If no pagination control found, try to find it
                FindAndConnectPaginationControl();
            }

            // Load the first page
            LoadPageData();
        }

        private void FindAndConnectPaginationControl()
        {
            try
            {
                // Try to find pagination control in parent
                var parentForm = this.FindForm();
                if (parentForm != null)
                {
                    PaginationControl = FindControlRecursive<Inventory_Pagination>(parentForm);
                    if (PaginationControl != null)
                    {
                        PaginationControl.AlwaysShowPagination = true;
                        PaginationControl.InitializePagination(allProductsData, dgvInventoryList, 10);
                        PaginationControl.PageChanged += PaginationControl_PageChanged;
                        PaginationControl.ForceShow();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error finding pagination control: {ex.Message}");
            }
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

        private void PaginationHelper_PageChanged(object sender, EventArgs e)
        {
            LoadPageData();
        }

        private void PaginationControl_PageChanged(object sender, int pageNumber)
        {
            if (paginationHelper != null)
            {
                paginationHelper.GoToPage(pageNumber);
            }
        }

        private void LoadPageData()
        {
            if (paginationHelper == null) return;

            try
            {
                dgvInventoryList.Rows.Clear();

                var pageData = paginationHelper.GetCurrentPageData();

                foreach (DataRow row in pageData.Rows)
                {
                    string productName = row["product_name"].ToString();
                    string sku = row["SKU"].ToString();
                    string category = row["category_name"].ToString();
                    int currentStock = Convert.ToInt32(row["current_stock"]);
                    int reorderPoint = Convert.ToInt32(row["reorder_point"]);
                    string status = row["status"].ToString();
                    string imagePath = row["image_path"].ToString();
                    string brand = row["brand"].ToString();

                    Image productImage = ProductImageManager.GetProductImage(imagePath);
                    Image adjustStockIcon = Properties.Resources.AdjustStock;
                    Image deactivateIcon = Properties.Resources.Deactivate_Circle1;
                    Image viewDetailsIcon = Properties.Resources.Group_10481;

                    int rowIndex = dgvInventoryList.Rows.Add(
                        productName,
                        productImage,
                        category,
                        currentStock,
                        reorderPoint,
                        status,
                        adjustStockIcon,
                        deactivateIcon,
                        viewDetailsIcon
                    );

                    // Store data in the row's Tag property
                    if (rowIndex >= 0 && rowIndex < dgvInventoryList.Rows.Count)
                    {
                        var rowData = new InventoryRowData
                        {
                            ImagePath = imagePath,
                            SKU = sku,
                            Brand = brand
                        };
                        dgvInventoryList.Rows[rowIndex].Tag = rowData;
                    }
                }

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

            // Adjust Stock button column (column 6)
            if (e.ColumnIndex == 6)
            {
                try
                {
                    string productName = dgvInventoryList.Rows[e.RowIndex].Cells[0].Value?.ToString() ?? "";
                    string category = dgvInventoryList.Rows[e.RowIndex].Cells[2].Value?.ToString() ?? "";

                    // Parse current stock safely
                    int currentStock = 0;
                    if (dgvInventoryList.Rows[e.RowIndex].Cells[3].Value != null)
                    {
                        int.TryParse(dgvInventoryList.Rows[e.RowIndex].Cells[3].Value.ToString(), out currentStock);
                    }

                    // Get image path, SKU, and brand from row Tag
                    string imagePath = "";
                    string sku = "";
                    string brand = "";

                    if (dgvInventoryList.Rows[e.RowIndex].Tag is InventoryRowData rowData)
                    {
                        imagePath = rowData.ImagePath ?? "";
                        sku = rowData.SKU ?? "";
                        brand = rowData.Brand ?? "";
                    }

                    // Try to get the main page and call ShowAdjustStockForProduct
                    var mainPage = FindParentOfType<InventoryMainPage>(this);
                    if (mainPage != null && !string.IsNullOrEmpty(productName))
                    {
                        mainPage.ShowAdjustStockForProduct(productName, sku, brand, currentStock, imagePath);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening adjust stock: {ex.Message}");
                }
            }

            // Deactivate/Activate button column (column 7)
            else if (e.ColumnIndex == 7)
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

                    // Determine new status based on current status
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

                    // Ask for confirmation
                    DialogResult result = MessageBox.Show(confirmationMessage,
                        $"Confirm {actionText}",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Update the product status in database
                        bool success = UpdateProductStatus(productName, newActiveStatus);

                        if (success)
                        {
                            MessageBox.Show($"Product '{productName}' has been {actionText}d successfully!",
                                          "Success",
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Information);

                            // Refresh the data to show updated status
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

            // View Details button column (column 8)
            else if (e.ColumnIndex == 8)
            {
                try
                {
                    string productName = dgvInventoryList.Rows[e.RowIndex].Cells[0].Value?.ToString() ?? "";
                    string category = dgvInventoryList.Rows[e.RowIndex].Cells[2].Value?.ToString() ?? "";
                    string currentStatus = dgvInventoryList.Rows[e.RowIndex].Cells[5].Value?.ToString() ?? "";

                    // Parse current stock safely
                    int currentStock = 0;
                    if (dgvInventoryList.Rows[e.RowIndex].Cells[3].Value != null)
                    {
                        int.TryParse(dgvInventoryList.Rows[e.RowIndex].Cells[3].Value.ToString(), out currentStock);
                    }

                    // Get image path, SKU, and brand from row Tag
                    string imagePath = "";
                    string sku = "";
                    string brand = "";

                    if (dgvInventoryList.Rows[e.RowIndex].Tag is InventoryRowData rowData)
                    {
                        imagePath = rowData.ImagePath ?? "";
                        sku = rowData.SKU ?? "";
                        brand = rowData.Brand ?? "";
                    }

                    // Try to get the main page and call ShowItemDescription
                    var mainPage = FindParentOfType<InventoryMainPage>(this);
                    if (mainPage != null && !string.IsNullOrEmpty(productName))
                    {
                        mainPage.ShowItemDescriptionForProduct(productName, sku, category, currentStock, brand, imagePath);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening item description: {ex.Message}");
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

        private Image LoadProductImage(string imageFileName)
        {
            return ProductImageManager.GetProductImage(imageFileName);
        }

        private Image GetDefaultImage()
        {
            Bitmap defaultImage = new Bitmap(50, 50);
            using (Graphics g = Graphics.FromImage(defaultImage))
            {
                g.Clear(Color.LightGray);
                using (Font font = new Font("Arial", 8))
                using (Brush brush = new SolidBrush(Color.DarkGray))
                {
                    g.DrawString("No Image", font, brush, 5, 15);
                }
            }
            return defaultImage;
        }

        // Clean up images when rows are removed
        private void dgvInventoryList_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            for (int i = e.RowIndex; i < e.RowIndex + e.RowCount; i++)
            {
                if (i >= 0 && i < dgvInventoryList.Rows.Count)
                {
                    var row = dgvInventoryList.Rows[i];
                    // Only dispose product images (column 1), not resource icons
                    if (row.Cells[1].Value is Image image && image != null)
                    {
                        // Only dispose if it's not the default image
                        if (!IsDefaultImage(image))
                        {
                            image.Dispose();
                        }
                    }
                }
            }
        }

        private bool IsDefaultImage(Image image)
        {
            try
            {
                if (image.Width == 50 && image.Height == 50)
                {
                    using (Bitmap bmp = new Bitmap(image))
                    {
                        // Check if it's our default light gray image
                        Color centerPixel = bmp.GetPixel(25, 25);
                        return centerPixel.R == 211 && centerPixel.G == 211 && centerPixel.B == 211;
                    }
                }
            }
            catch
            {
                // Ignore errors
            }
            return false;
        }

        // Public method to refresh pagination when data changes
        public void RefreshPagination()
        {
            LoadDataFromDatabase();
        }
    }
}