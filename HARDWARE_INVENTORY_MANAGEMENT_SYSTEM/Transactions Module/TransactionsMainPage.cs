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
        private Walk_inCartDetails cartDetails;

        public TransactionsMainPage()
        {
            InitializeComponent();
            productData = new ProductDataAccess();
            this.BackColor = Color.White;

            InitializePagination();
            InitializeSearch();

            // Initialize cart panel - dock CartDetails in panel1
            InitializeCartPanel();

            // Now load products (cart reference is ready)
            LoadProducts();
        }

        private void InitializeCartPanel()
        {
            // Make sure panel1 exists and configure it
            if (panel1 != null)
            {
                panel1.Dock = DockStyle.Right;
                panel1.Width = 294;
                panel1.BackColor = Color.White;
                panel1.Padding = new Padding(0);
                panel1.BringToFront(); // Ensure panel1 is on top

                // Clear any existing controls in panel1
                panel1.Controls.Clear();

                // Create the CartDetails control
                CartDetails cartDetailsPanel = new CartDetails();
                cartDetailsPanel.Name = "cartDetailsPanel";
                cartDetailsPanel.Dock = DockStyle.Fill; // Fill the entire panel1
                cartDetailsPanel.BackColor = Color.White;

                // Add CartDetails to panel1
                panel1.Controls.Add(cartDetailsPanel);

                // Get cart reference with retry logic
                cartDetailsPanel.Load += (s, e) =>
                {
                    this.cartDetails = cartDetailsPanel.GetCurrentWalkInCart();

                    if (this.cartDetails != null)
                    {
                        Console.WriteLine("✓ Cart reference obtained!");
                        UpdateAllProductControlsWithCartReference();
                    }
                    else
                    {
                        Console.WriteLine("⚠ Waiting for cart...");
                        System.Threading.Tasks.Task.Delay(200).ContinueWith(_ =>
                        {
                            this.Invoke((Action)(() =>
                            {
                                this.cartDetails = cartDetailsPanel.GetCurrentWalkInCart();
                                if (this.cartDetails != null)
                                {
                                    Console.WriteLine("✓ Cart obtained (retry)!");
                                    UpdateAllProductControlsWithCartReference();
                                }
                            }));
                        });
                    }
                };

                Console.WriteLine("✓ CartDetails docked in panel1!");
                Console.WriteLine($"✓ Panel1 size: {panel1.Size}");
                Console.WriteLine($"✓ Panel1 location: {panel1.Location}");
            }
            else
            {
                Console.WriteLine("⚠ panel1 not found! Make sure it exists in the Designer.");
            }
        }

        // Initialize pagination control
        private void InitializePagination()
        {
            pagination = new PaginationTransation();
            pagination.Location = new Point(28, this.Height - 80);
            pagination.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            pagination.Visible = true;
            pagination.PageChanged += Pagination_PageChanged;
            pagination.AlwaysShowPagination = true;

            this.Controls.Add(pagination);
            pagination.BringToFront();
        }

        // Initialize search functionality
        private void InitializeSearch()
        {
            TextBox searchBox = FindTextBoxInSearchField(transactionsSearchBar1);

            if (searchBox != null)
            {
                searchTextBox = new SearchTextBox(searchBox, "Search products...");
                searchTextBox.SearchTextChanged += SearchTextBox_SearchTextChanged;
            }
        }

        // Set cart reference - now optional since cart is created internally
        public void SetCartReference(Walk_inCartDetails cart)
        {
            // Optional: Allow external cart reference override
            if (cart != null)
            {
                this.cartDetails = cart;
                UpdateAllProductControlsWithCartReference();

                Console.WriteLine($"✓ External cart reference set!");
                Console.WriteLine($"✓ Cart Type: {cart.GetType().Name}");
            }
        }

        // Update all product controls with current cart reference
        private void UpdateAllProductControlsWithCartReference()
        {
            foreach (Control control in MainpageProductLayout.Controls)
            {
                if (control is Products productControl)
                {
                    productControl.SetCartReference(cartDetails);
                }
            }
        }

        // Handle search text changes
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

        // Load all active products from database
        private void LoadProducts()
        {
            products = productData.GetActiveProducts();
            ConvertProductsToDataTable();
            InitializeOrUpdatePagination();
        }

        // Search products by search term
        private void SearchProducts(string searchTerm)
        {
            products = productData.SearchProducts(searchTerm);
            ConvertProductsToDataTable();
            InitializeOrUpdatePagination();
        }

        // Convert product list to DataTable for pagination
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

        // Initialize or update pagination with current data
        private void InitializeOrUpdatePagination()
        {
            if (pagination != null && productsDataTable != null)
            {
                pagination.InitializePagination(productsDataTable, null, 12);
                pagination.ForceShow();
                DisplayCurrentPage();
            }
            else if (pagination != null)
            {
                pagination.ForceShow();
                DisplayCurrentPage();
            }
        }

        // Handle page change event
        private void Pagination_PageChanged(object sender, int pageNumber)
        {
            DisplayCurrentPage();
        }

        // Display products for current page
        private void DisplayCurrentPage()
        {
            if (pagination == null) return;

            MainpageProductLayout.Controls.Clear();
            DataTable currentPageData = pagination.GetCurrentPageData();

            if (currentPageData == null || currentPageData.Rows.Count == 0)
            {
                Label noProductsLabel = new Label()
                {
                    Text = "No products available",
                    TextAlign = ContentAlignment.MiddleCenter,
                    Dock = DockStyle.Fill,
                    Font = new Font("Segoe UI", 14, FontStyle.Regular),
                    ForeColor = Color.Gray
                };
                MainpageProductLayout.Controls.Add(noProductsLabel);
                return;
            }

            // Create product controls
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
                    Margin = new Padding(5)
                };

                productControl.Image = ProductImageManager.GetProductImage(product.ImagePath);

                // Set cart reference for each product
                if (cartDetails != null)
                {
                    productControl.SetCartReference(cartDetails);
                }

                productControl.ProductAddedToCart += ProductControl_ProductAddedToCart;
                MainpageProductLayout.Controls.Add(productControl);
            }
        }

        // Handle product added to cart event
        private void ProductControl_ProductAddedToCart(object sender, Product product)
        {
            if (cartDetails != null)
            {
                try
                {
                    cartDetails.AddProductToCartById(product.ProductInternalID, 1);
                    MessageBox.Show($"Added {product.ProductName} to cart!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding to cart: {ex.Message}", "Cart Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show($"Cart is NULL!\n\nProduct: {product.ProductName}\nID: {product.ProductInternalID}",
                    "Cart Error - DEBUG", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Find TextBox control recursively
        private TextBox FindTextBoxInSearchField(Control searchFieldControl)
        {
            return FindControlRecursive<TextBox>(searchFieldControl);
        }

        // Generic recursive control finder
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

        // Public method to refresh product list
        public void RefreshProducts()
        {
            LoadProducts();
        }

        private void MainpageProductLayout_Paint(object sender, PaintEventArgs e)
        {
        }

        private void cartDetails1_Load(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}