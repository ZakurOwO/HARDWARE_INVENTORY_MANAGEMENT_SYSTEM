using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Properties;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module
{
    public partial class AddNewItem_Form : UserControl
    {
        private string imageFilePath = "";

        public AddNewItem_Form()
        {
            InitializeComponent();
            LoadCategoriesAndUnits();
            InitializeForm();

            // Enable mouse wheel scrolling
            this.AutoScroll = true;
            this.MouseWheel += AddNewItem_Form_MouseWheel;
        }

        private void AddNewItem_Form_MouseWheel(object sender, MouseEventArgs e)
        {
            if (this.VerticalScroll.Visible)
            {
                int newScrollPosition = this.VerticalScroll.Value - (e.Delta / 2);
                this.VerticalScroll.Value = Math.Max(this.VerticalScroll.Minimum,
                    Math.Min(this.VerticalScroll.Maximum, newScrollPosition));
            }
        }

        private void LoadCategoriesAndUnits()
        {
            try
            {
                // Load categories
                DataTable categories = InventoryDatabaseHelper.LoadCategoriesFromDatabase();
                CategoryCombobox.Items.Clear();
                foreach (DataRow row in categories.Rows)
                {
                    CategoryCombobox.Items.Add(new ComboboxItem
                    {
                        Text = row["category_name"].ToString(),
                        Value = row["CategoryID"].ToString()
                    });
                }

                // Load units
                DataTable units = InventoryDatabaseHelper.LoadUnitsFromDatabase();
                UnitOfMeasurementComboBox.Items.Clear();
                foreach (DataRow row in units.Rows)
                {
                    UnitOfMeasurementComboBox.Items.Add(new ComboboxItem
                    {
                        Text = row["unit_name"].ToString(),
                        Value = row["UnitID"].ToString()
                    });
                }

                // Load status options
                StatusComboBox.Items.Clear();
                StatusComboBox.Items.Add("Active");
                StatusComboBox.Items.Add("Inactive");
                StatusComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeForm()
        {
            // Set default values
            nudCurrentStock.Value = 0;
            nudMinimumStock.Value = 0;
            nudCostPrice.Value = 0;
            nudSellingPrice.Value = 0;
            ExpirationDataComboBox.Value = DateTime.Now.AddYears(1);
        }

        private void closeButton1_Click(object sender, EventArgs e)
        {
            // Simply trigger the event - the container will handle cleanup
            OnProductAdded?.Invoke(this, EventArgs.Empty);
        }

        private void tbxImageUpload_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Images|*.png;*.jpg;*.jpeg;*.gif;*.bmp";
                ofd.Title = "Select Product Image";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    imageFilePath = ofd.FileName;
                    ImageUploadBox.Text = Path.GetFileName(ofd.FileName);

                    // Show preview (optional)
                    try
                    {
                        // If you have a PictureBox for preview, uncomment this:
                        // pictureBoxPreview.Image = Image.FromFile(imageFilePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading image preview: {ex.Message}",
                                      "Image Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                AddProduct();
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(ProductNametxtbox.Text))
            {
                MessageBox.Show("Please enter product name.", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ProductNametxtbox.Focus();
                return false;
            }

            if (CategoryCombobox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a category.", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CategoryCombobox.Focus();
                return false;
            }

            if (UnitOfMeasurementComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a unit of measurement.", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                UnitOfMeasurementComboBox.Focus();
                return false;
            }

            if (nudCurrentStock.Value < 0)
            {
                MessageBox.Show("Current stock cannot be negative.", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudCurrentStock.Focus();
                return false;
            }

            if (nudMinimumStock.Value < 0)
            {
                MessageBox.Show("Minimum stock cannot be negative.", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudMinimumStock.Focus();
                return false;
            }

            // Check for duplicate product name
            if (InventoryDatabaseHelper.IsProductNameExists(ProductNametxtbox.Text.Trim()))
            {
                MessageBox.Show("Product name already exists. Please choose a different name.",
                              "Duplicate Product", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ProductNametxtbox.Focus();
                ProductNametxtbox.SelectAll();
                return false;
            }

            return true;
        }

        private void AddProduct()
        {
            try
            {
                string productName = ProductNametxtbox.Text.Trim();
                string description = DescriptionRichTextBox.Text.Trim();
                string categoryId = (CategoryCombobox.SelectedItem as ComboboxItem)?.Value;
                string unitId = (UnitOfMeasurementComboBox.SelectedItem as ComboboxItem)?.Value;
                int currentStock = (int)nudCurrentStock.Value;
                int reorderPoint = (int)nudMinimumStock.Value;
                bool active = StatusComboBox.SelectedItem?.ToString() == "Active";

                // SIMPLE IMAGE HANDLING
                string imageFileName = "";
                if (!string.IsNullOrEmpty(imageFilePath))
                {
                    // Just use the original filename
                    imageFileName = Path.GetFileName(imageFilePath);

                    // Create ImageInventory folder if it doesn't exist
                    string imageFolder = Path.Combine(Application.StartupPath, "ImageInventory");
                    if (!Directory.Exists(imageFolder))
                    {
                        Directory.CreateDirectory(imageFolder);
                    }

                    // Copy image to ImageInventory folder
                    string destPath = Path.Combine(imageFolder, imageFileName);
                    File.Copy(imageFilePath, destPath, true);

                    Console.WriteLine($"Image copied to: {destPath}");
                }

                bool success = InventoryDatabaseHelper.AddProduct(
                    productName, description, categoryId, unitId,
                    currentStock, imageFileName, reorderPoint, active,
                    out string productId, out string generatedSku
                );

                if (success)
                {
                    TryLogInventoryCreate(productId, productName, generatedSku, categoryId, unitId, currentStock, reorderPoint, active);

                    MessageBox.Show("Product added successfully!", "Success",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    OnProductAdded?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    MessageBox.Show("Failed to add product. Please try again.", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding product: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            ProductNametxtbox.Clear();
            SKUtxtbox.Clear();
            Brandtxtbox.Clear();
            DescriptionRichTextBox.Clear();
            ImageUploadBox.Clear();
            CategoryCombobox.SelectedIndex = -1;
            UnitOfMeasurementComboBox.SelectedIndex = -1;
            StatusComboBox.SelectedIndex = 0;
            nudCurrentStock.Value = 0;
            nudMinimumStock.Value = 0;
            nudCostPrice.Value = 0;
            nudSellingPrice.Value = 0;
            ExpirationDataComboBox.Value = DateTime.Now.AddYears(1);
            imageFilePath = "";
            ProductNametxtbox.Focus();
        }

        private void TryLogInventoryCreate(string productId, string productName, string sku, string categoryId, string unitId, int stock, int reorderPoint, bool active)
        {
            try
            {
                string newValues = $"Name={productName}; SKU={sku}; CategoryID={categoryId}; UnitID={unitId}; Stock={stock}; ReorderPoint={reorderPoint}; Active={active}";

                AuditHelper.LogWithDetails(
                    AuditModule.INVENTORY,
                    $"Created product {productName}",
                    AuditActivityType.CREATE,
                    tableAffected: "Products",
                    recordId: productId,
                    newValues: newValues
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Inventory audit log failed: {ex.Message}");
            }
        }

        // Event for when product is successfully added
        public event EventHandler OnProductAdded;

        // Empty event handlers
        private void ProductNametxtbox_TextChanged(object sender, EventArgs e) { }
        private void SKUtxtbox_TextChanged(object sender, EventArgs e) { }
        private void CategoryCombobox_SelectedIndexChanged(object sender, EventArgs e) { }
        private void Brandtxtbox_TextChanged(object sender, EventArgs e) { }
        private void DescriptionRichTextBox_TextChanged(object sender, EventArgs e) { }
        private void UnitOfMeasurementComboBox_SelectedIndexChanged(object sender, EventArgs e) { }
        private void ImageUploadBox_TextChanged(object sender, EventArgs e) { }
        private void nudCurrentStock_ValueChanged(object sender, EventArgs e) { }
        private void nudMinimumStock_ValueChanged(object sender, EventArgs e) { }
        private void nudCostPrice_ValueChanged(object sender, EventArgs e) { }
        private void nudSellingPrice_ValueChanged(object sender, EventArgs e) { }
        private void StatusComboBox_SelectedIndexChanged(object sender, EventArgs e) { }
        private void ExpirationDataComboBox_ValueChanged(object sender, EventArgs e) { }
        private void PrimarySupplierComboBox_SelectedIndexChanged(object sender, EventArgs e) { }

        private void closeButton1_Load(object sender, EventArgs e)
        {

        }
    }
}