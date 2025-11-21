using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Models;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Data;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module
{
    public partial class TransactionsMainPage : UserControl
    {
        private ProductDataAccess productData;
        private List<Product> products;
        private SearchTextBox searchTextBox;
        private PaginationTransation pagination;
        private DataTable productsDataTable;

        public TransactionsMainPage()
        {
            InitializeComponent();
            productData = new ProductDataAccess();
            InitializePagination();
            InitializeSearch();
            LoadProducts();
        }

        private void InitializePagination()
        {
            // Create and configure pagination control
            pagination = new PaginationTransation();

            // Set the specific location (28, 600)
            pagination.Location = new Point(28, 620);

            // Remove Dock style and set manual positioning
            pagination.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            pagination.Visible = true; // Always visible
            pagination.PageChanged += Pagination_PageChanged;

            // Set to always show pagination regardless of data
            pagination.AlwaysShowPagination = true;

            // Add to main container
            this.Controls.Add(pagination);
            pagination.BringToFront();
        }

        private void InitializeSearch()
        {
            // Find the search textbox in your search field control
            TextBox searchBox = FindTextBoxInSearchField(transactionsSearchBar1);

            if (searchBox != null)
            {
                searchTextBox = new SearchTextBox(searchBox, "Search products...");
                searchTextBox.SearchTextChanged += SearchTextBox_SearchTextChanged;
            }
        }

        private void SearchTextBox_SearchTextChanged(object sender, string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                LoadProducts();
            }
            else
            {
                SearchProducts(searchText);
            }
        }

        private void LoadProducts()
        {
            products = productData.GetActiveProducts();
            ConvertProductsToDataTable();
            InitializeOrUpdatePagination();
        }

        private void SearchProducts(string searchTerm)
        {
            products = productData.SearchProducts(searchTerm);
            ConvertProductsToDataTable();
            InitializeOrUpdatePagination();
        }

        private void ConvertProductsToDataTable()
        {
            productsDataTable = new DataTable();
            productsDataTable.Columns.Add("ProductInternalID", typeof(int));
            productsDataTable.Columns.Add("ProductID", typeof(string));
            productsDataTable.Columns.Add("ProductName", typeof(string));
            productsDataTable.Columns.Add("SKU", typeof(string));
            productsDataTable.Columns.Add("Description", typeof(string));
            productsDataTable.Columns.Add("CategoryID", typeof(string));
            productsDataTable.Columns.Add("UnitID", typeof(string));
            productsDataTable.Columns.Add("CurrentStock", typeof(int));
            productsDataTable.Columns.Add("ImagePath", typeof(string));
            productsDataTable.Columns.Add("SellingPrice", typeof(decimal));
            productsDataTable.Columns.Add("Active", typeof(bool));

            foreach (var product in products)
            {
                productsDataTable.Rows.Add(
                    product.ProductInternalID,
                    product.ProductID,
                    product.ProductName,
                    product.SKU,
                    product.Description,
                    product.CategoryID,
                    product.UnitID,
                    product.CurrentStock,
                    product.ImagePath,
                    product.SellingPrice,
                    product.Active
                );
            }
        }

        private void InitializeOrUpdatePagination()
        {
            if (pagination != null && productsDataTable != null)
            {
                // Initialize pagination with 4 products per page
                pagination.InitializePagination(productsDataTable, null, 4);
                pagination.ForceShow(); // Force show pagination

                // Load first page
                DisplayCurrentPage();
            }
            else if (pagination != null)
            {
                // Even with no data, keep pagination visible
                pagination.ForceShow();
                DisplayCurrentPage();
            }
        }

        private void Pagination_PageChanged(object sender, int pageNumber)
        {
            DisplayCurrentPage();
        }

        private void DisplayCurrentPage()
        {
            if (pagination == null) return;

            // Clear existing products
            MainpageProductLayout.Controls.Clear();

            // Get current page data
            DataTable currentPageData = pagination.GetCurrentPageData();

            if (currentPageData == null || currentPageData.Rows.Count == 0)
            {
                // Show message when no products found
                Label noProductsLabel = new Label()
                {
                    Text = "No products available",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    Font = new Font("Arial", 14, FontStyle.Bold),
                    ForeColor = Color.Gray
                };
                MainpageProductLayout.Controls.Add(noProductsLabel);
                return;
            }

            // Create product controls for each product in current page
            foreach (DataRow row in currentPageData.Rows)
            {
                var product = new Product
                {
                    ProductInternalID = Convert.ToInt32(row["ProductInternalID"]),
                    ProductID = row["ProductID"].ToString(),
                    ProductName = row["ProductName"].ToString(),
                    SKU = row["SKU"].ToString(),
                    Description = row["Description"].ToString(),
                    CategoryID = row["CategoryID"].ToString(),
                    UnitID = row["UnitID"].ToString(),
                    CurrentStock = Convert.ToInt32(row["CurrentStock"]),
                    ImagePath = row["ImagePath"].ToString(),
                    SellingPrice = Convert.ToDecimal(row["SellingPrice"]),
                    Active = Convert.ToBoolean(row["Active"])
                };

                var productControl = new Products()
                {
                    Product_Name = product.ProductName,
                    Price = product.SellingPrice,
                    Stock = product.CurrentStock,
                    ProductData = product,
                    Margin = new Padding(3)
                };

                // Load product image
                var productImage = ProductImageManager.GetProductImage(product.ImagePath);
                productControl.Image = productImage;

                // Handle add to cart event
                productControl.ProductAddedToCart += ProductControl_ProductAddedToCart;

                // Add to layout
                MainpageProductLayout.Controls.Add(productControl);
            }
        }

        private void ProductControl_ProductAddedToCart(object sender, Product product)
        {
            // Handle add to cart functionality
            AddToShoppingCart(product);
        }

        private void AddToShoppingCart(Product product)
        {
            // Implement your shopping cart logic here
            MessageBox.Show($"Added {product.ProductName} to cart!", "Success",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        // Public method to refresh products
        public void RefreshProducts()
        {
            LoadProducts();
        }

        private void MainpageProductLayout_Paint(object sender, PaintEventArgs e)
        {
            // Optional: Custom painting for the layout panel
        }
    }
}