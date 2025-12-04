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
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components.ClassComponentTransaction;

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
        private DeliveryCartDetails deliveryCartDetails;
        private ItemDescription_PopUp itemDescriptionPopup;

        public TransactionsMainPage()
        {
            InitializeComponent();
            productData = new ProductDataAccess();
            this.BackColor = Color.White;

            Console.WriteLine("✓ TransactionsMainPage initialized");

            InitializePagination();
            InitializeSearch();
            InitializeCartPanel();
            LoadProducts();
            InitializeItemDescriptionPopup();

            SharedCartManager.Instance.InventoryUpdated += (s, e) => LoadProducts(); // auto-refresh inventory cards when stock changes
            this.Disposed += (s, e) => SharedCartManager.Instance.ClearCart(); // ensure cart state is discarded when leaving page
        }

        private void InitializeItemDescriptionPopup()
        {
            // Create the popup
            itemDescriptionPopup = new ItemDescription_PopUp();
            itemDescriptionPopup.Visible = false;
            itemDescriptionPopup.Location = new Point(13, 148); // Set to your desired location

            // Handle add to cart from popup
            itemDescriptionPopup.AddToCartRequested += (sender, e) =>
            {
                HandlePopupAddToCart(e.Product, e.Quantity);
            };

            // Add to the main form controls
            this.Controls.Add(itemDescriptionPopup);
            itemDescriptionPopup.BringToFront();

            Console.WriteLine("✓ ItemDescription_PopUp initialized at location (13, 148)");
        }

        private void HandlePopupAddToCart(Product product, int quantity)
        {
            var cartDetailsPanel = panel1?.Controls?["cartDetailsPanel"] as CartDetails;

            if (cartDetailsPanel != null)
            {
                if (cartDetailsPanel.IsWalkInCartActive())
                {
                    var walkInCart = cartDetailsPanel.GetCurrentWalkInCart();
                    if (walkInCart != null)
                    {
                        for (int i = 0; i < quantity; i++)
                        {
                            walkInCart.AddProductToCartById(product.ProductInternalID, 1);
                        }
                        return;
                    }
                }
                else if (cartDetailsPanel.IsDeliveryCartActive())
                {
                    var deliveryCart = cartDetailsPanel.GetCurrentDeliveryCart();
                    if (deliveryCart != null)
                    {
                        for (int i = 0; i < quantity; i++)
                        {
                            deliveryCart.AddProductToCartById(product.ProductInternalID, 1);
                        }
                        return;
                    }
                }

                MessageBox.Show("Please select a cart type (Walk-In or Delivery)!", "Cart Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Cart is not available. Please try reloading the page.", "Cart Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ShowProductDetailsPopup(Product product)
        {
            if (itemDescriptionPopup == null)
            {
                Console.WriteLine("ERROR: itemDescriptionPopup is null!");
                return;
            }

            // Set product details
            itemDescriptionPopup.SetProductDetails(product);

            // Ensure location is correct
            itemDescriptionPopup.Location = new Point(13, 148);

            // Make it visible and bring to front
            itemDescriptionPopup.Visible = true;
            itemDescriptionPopup.BringToFront();

            Console.WriteLine($"✓ Product details popup shown at (13, 148) for: {product.ProductName}");
        }

        private void HideProductDetailsPopup()
        {
            if (itemDescriptionPopup != null)
            {
                itemDescriptionPopup.Visible = false;
                Console.WriteLine("✓ Product details popup hidden");
            }
        }

        private void InitializeCartPanel()
        {
            if (panel1 != null)
            {
                panel1.Dock = DockStyle.Right;
                panel1.Width = 290;
                panel1.BackColor = Color.White;
                panel1.Padding = new Padding(0);
                panel1.BringToFront();

                panel1.Controls.Clear();

                CartDetails cartDetailsPanel = new CartDetails();
                cartDetailsPanel.Name = "cartDetailsPanel";
                cartDetailsPanel.Dock = DockStyle.Fill;
                cartDetailsPanel.BackColor = Color.White;

                panel1.Controls.Add(cartDetailsPanel);

                cartDetailsPanel.Load += (s, e) =>
                {
                    this.cartDetails = cartDetailsPanel.GetCurrentWalkInCart();
                    this.deliveryCartDetails = cartDetailsPanel.GetCurrentDeliveryCart();

                    if (this.cartDetails != null || this.deliveryCartDetails != null)
                    {
                        Console.WriteLine("✓ Cart reference obtained!");
                        UpdateAllProductControlsWithCartReference();
                    }
                };

                Console.WriteLine("✓ CartDetails docked in panel1");
            }
        }

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
            Console.WriteLine("✓ Pagination initialized");
        }

        private void InitializeSearch()
        {
            TextBox searchBox = FindTextBoxInSearchField(transactionsSearchBar1);

            if (searchBox != null)
            {
                searchTextBox = new SearchTextBox(searchBox, "Search products...");
                searchTextBox.SearchTextChanged += SearchTextBox_SearchTextChanged;
                Console.WriteLine("✓ Search initialized");
            }
        }

        private void LoadProducts()
        {
            try
            {
                products = productData.GetActiveProducts();
                Console.WriteLine($"✓ Loaded {products.Count} active products from database");

                if (products.Count == 0)
                {
                    ShowNoProductsMessage();
                    return;
                }

                ConvertProductsToDataTable();
                InitializeOrUpdatePagination();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"✗ Error loading products: {ex.Message}");
                ShowNoProductsMessage();
            }
        }

        private void ShowNoProductsMessage()
        {
            MainpageProductLayout.Controls.Clear();
            Label noProductsLabel = new Label()
            {
                Text = "No products available",
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 14, FontStyle.Regular),
                ForeColor = Color.Gray,
                Height = 100,
                Width = 600
            };
            MainpageProductLayout.Controls.Add(noProductsLabel);
        }

        private void SearchProducts(string searchTerm)
        {
            try
            {
                products = productData.SearchProducts(searchTerm);
                Console.WriteLine($"✓ Found {products.Count} products matching '{searchTerm}'");

                if (products.Count == 0)
                {
                    ShowNoSearchResultsMessage(searchTerm);
                    return;
                }

                ConvertProductsToDataTable();
                InitializeOrUpdatePagination();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching products: {ex.Message}", "Search Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowNoSearchResultsMessage(string searchTerm)
        {
            MainpageProductLayout.Controls.Clear();
            Label noResultsLabel = new Label()
            {
                Text = $"No products found for '{searchTerm}'",
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 14, FontStyle.Regular),
                ForeColor = Color.Gray,
                Height = 100,
                Width = 600
            };
            MainpageProductLayout.Controls.Add(noResultsLabel);
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

            Console.WriteLine($"✓ Converted {productsDataTable.Rows.Count} products to DataTable");
        }

        private void InitializeOrUpdatePagination()
        {
            if (pagination != null && productsDataTable != null)
            {
                pagination.InitializePagination(productsDataTable, null, 12);
                pagination.ForceShow();
                DisplayCurrentPage();
            }
        }

        private void DisplayCurrentPage()
        {
            if (pagination == null)
            {
                Console.WriteLine("✗ Pagination is null");
                return;
            }

            MainpageProductLayout.Controls.Clear();
            DataTable currentPageData = pagination.GetCurrentPageData();

            if (currentPageData == null || currentPageData.Rows.Count == 0)
            {
                Console.WriteLine("✗ No products to display");
                ShowNoProductsMessage();
                return;
            }

            Console.WriteLine($"✓ Displaying {currentPageData.Rows.Count} products on current page");

            foreach (DataRow row in currentPageData.Rows)
            {
                try
                {
                    var product = new Product
                    {
                        ProductInternalID = Convert.ToInt32(row["ProductInternalID"]),
                        ProductID = row["ProductID"].ToString(),
                        ProductName = row["ProductName"].ToString(),
                        SKU = row["SKU"].ToString(),
                        Description = row["Description"]?.ToString() ?? "",
                        CategoryID = row["CategoryID"].ToString(),
                        UnitID = row["UnitID"].ToString(),
                        CurrentStock = Convert.ToInt32(row["CurrentStock"]),
                        ImagePath = row["ImagePath"]?.ToString() ?? "",
                        SellingPrice = Convert.ToDecimal(row["SellingPrice"]),
                        Active = Convert.ToBoolean(row["Active"])
                    };

                    var productControl = new Products()
                    {
                        Product_Name = product.ProductName,
                        Price = product.SellingPrice,
                        Stock = product.CurrentStock,
                        ProductData = product,
                        Margin = new Padding(5),
                        Size = new Size(146, 136)
                    };

                    // Load product image
                    try
                    {
                        productControl.Image = ProductImageManager.GetProductImage(product.ImagePath);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"✗ Error loading image for {product.ProductName}: {ex.Message}");
                    }

                    // Set cart reference
                    if (cartDetails != null)
                    {
                        productControl.SetCartReference(cartDetails);
                    }

                    // Connect product details event - show popup at (13, 148)
                    productControl.ProductDetailsRequested += (sender, e) =>
                    {
                        Console.WriteLine($"Product details requested for: {e.ProductName}");
                        ShowProductDetailsPopup(e);
                    };

                    productControl.ProductAddedToCart += ProductControl_ProductAddedToCart;
                    MainpageProductLayout.Controls.Add(productControl);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"✗ Error creating product control: {ex.Message}");
                }
            }

            Console.WriteLine($"✓ Added {MainpageProductLayout.Controls.Count} product controls to layout");
        }

        private void ProductControl_ProductAddedToCart(object sender, Product product)
        {
            var cartDetailsPanel = panel1.Controls["cartDetailsPanel"] as CartDetails;

            if (cartDetailsPanel != null)
            {
                if (cartDetailsPanel.IsWalkInCartActive())
                {
                    var walkInCart = cartDetailsPanel.GetCurrentWalkInCart();
                    if (walkInCart != null)
                    {
                        walkInCart.AddProductToCartById(product.ProductInternalID, 1);
                        return;
                    }
                }
                else if (cartDetailsPanel.IsDeliveryCartActive())
                {
                    var deliveryCart = cartDetailsPanel.GetCurrentDeliveryCart();
                    if (deliveryCart != null)
                    {
                        deliveryCart.AddProductToCartById(product.ProductInternalID, 1);
                        return;
                    }
                }

                MessageBox.Show("Please select a cart type (Walk-In or Delivery)!", "Cart Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Cart is not available. Please try reloading the page.", "Cart Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

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

        private void Pagination_PageChanged(object sender, int pageNumber)
        {
            DisplayCurrentPage();
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

        public void RefreshProducts()
        {
            LoadProducts();
        }

        // Handle click outside the popup to close it
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (itemDescriptionPopup != null && itemDescriptionPopup.Visible)
            {
                // Check if click is outside the popup
                if (!itemDescriptionPopup.Bounds.Contains(e.Location))
                {
                    HideProductDetailsPopup();
                }
            }
        }

        // Add a test method to verify the popup works
        public void TestPopup()
        {
            var testProduct = new Product
            {
                ProductInternalID = 1,
                ProductName = "Test Product",
                SellingPrice = 99.99m,
                Description = "This is a test product description",
                CategoryName = "Test Category",
                CurrentStock = 10,
                ImagePath = ""
            };

            ShowProductDetailsPopup(testProduct);
        }

        private void MainpageProductLayout_Paint(object sender, PaintEventArgs e)
        {
            // Optional styling
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Optional styling
        }

        private void btnMainButtonIcon_Click(object sender, EventArgs e)
        {
            //refresh button
        }
    }
}