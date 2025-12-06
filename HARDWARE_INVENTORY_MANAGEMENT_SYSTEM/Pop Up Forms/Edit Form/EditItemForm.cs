using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Services;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module
{
    public partial class EditItem_Form : UserControl
    {
        private string currentProductId = "";
        private string imageFilePath = "";
        private string originalProductName = "";
        private string originalImagePath = "";
        private byte[] originalImageBytes;
        private byte[] selectedImageBytes;
        public Panel ParentScrollContainer { get; set; }

        // 🔥 Added overlay reference
        private PictureBox pcbBlurOverlay;
        private PictureBox imagePreviewBox;
        private bool hasProductImageColumn;

        public event EventHandler OnProductUpdated;

        // 🔥 Updated constructor to accept blur overlay
        public EditItem_Form(string productId, PictureBox blurOverlay)
        {
            InitializeComponent();
            currentProductId = productId;
            pcbBlurOverlay = blurOverlay;
            hasProductImageColumn = InventoryDatabaseHelper.ProductImageColumnExists();

            // Show overlay when form opens
            if (pcbBlurOverlay != null)
                pcbBlurOverlay.Visible = true;

            LoadCategoriesAndUnits();
            InitializeImagePreview();
            LoadProductData();
        }

        private void InitializeImagePreview()
        {
            imagePreviewBox = new PictureBox
            {
                Size = new Size(120, 120),
                Location = new Point(570, 440),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = ImageService.GetPlaceholderImage()
            };

            Controls.Add(imagePreviewBox);
            imagePreviewBox.BringToFront();
        }

        // ============================= LOAD CATEGORY + UNIT =============================
        private void LoadCategoriesAndUnits()
        {
            try
            {
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

                StatusComboBox.Items.Clear();
                StatusComboBox.Items.Add("Active");
                StatusComboBox.Items.Add("Inactive");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading categories/units: " + ex.Message);
            }
        }

        // ============================= LOAD EXISTING PRODUCT DATA =============================
        private void LoadProductData()
        {
            var details = InventoryDatabaseHelper.GetProductDetails(currentProductId);

            if (details == null)
            {
                MessageBox.Show("Unable to load product details.");
                return;
            }

            originalProductName = details.ProductName;

            ProductNametxtbox.Text = details.ProductName;
            SKUtxtbox.Text = details.SKU;
            DescriptionRichTextBox.Text = details.Description;
            
            nudMinimumStock.Value = details.ReorderPoint;
            nudSellingPrice.Text = details.SellingPrice.ToString("0.##");

            foreach (ComboboxItem item in CategoryCombobox.Items)
            {
                if (item.Text == details.CategoryName)
                {
                    CategoryCombobox.SelectedItem = item;
                    break;
                }
            }

            foreach (ComboboxItem item in UnitOfMeasurementComboBox.Items)
            {
                if (item.Text == details.UnitName)
                {
                    UnitOfMeasurementComboBox.SelectedItem = item;
                    break;
                }
            }

            StatusComboBox.SelectedItem = details.Active ? "Active" : "Inactive";
            selectedImageBytes = details.ProductImage;
            originalImageBytes = details.ProductImage;
            imageFilePath = details.ImagePath;
            originalImagePath = details.ImagePath;

            if (selectedImageBytes == null && !string.IsNullOrWhiteSpace(imageFilePath))
            {
                selectedImageBytes = ImageService.ConvertImageToBytes(ImageService.GetImage(imageFilePath, ImageCategory.Product));
            }

            if (!string.IsNullOrWhiteSpace(imageFilePath))
            {
                ImageUploadBox.Text = Path.GetFileName(imageFilePath);
            }

            UpdateImagePreview(selectedImageBytes, imageFilePath);

        }

        // ============================= PICK IMAGE =============================
        

       

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(ProductNametxtbox.Text))
            {
                MessageBox.Show("Product name cannot be empty.");
                return false;
            }

            if (ProductNametxtbox.Text.Trim() != originalProductName &&
                InventoryDatabaseHelper.IsProductNameExists(ProductNametxtbox.Text.Trim()))
            {
                MessageBox.Show("Product name already exists.");
                return false;
            }

            if (CategoryCombobox.SelectedIndex == -1 ||
                UnitOfMeasurementComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Category and Unit are required.");
                return false;
            }

            return true;
        }

        private void UpdateProduct()
        {
            string newName = ProductNametxtbox.Text.Trim();
            string description = DescriptionRichTextBox.Text.Trim();
            string categoryId = (CategoryCombobox.SelectedItem as ComboboxItem).Value;
            string unitId = (UnitOfMeasurementComboBox.SelectedItem as ComboboxItem).Value;            
            int reorderPoint = (int)nudMinimumStock.Value;
            decimal sellingPrice = 0m;
            if (!decimal.TryParse(nudSellingPrice.Text.Trim(), out sellingPrice))
            {
                MessageBox.Show("Invalid selling price. Please enter a valid number.",
                                "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            bool active = StatusComboBox.SelectedItem.ToString() == "Active";

            string imageFileName = string.IsNullOrWhiteSpace(imageFilePath)
                ? ImageUploadBox.Text.Trim()
                : imageFilePath;

            using (SqlConnection conn = new SqlConnection(ConnectionString.DataSource))
            {
                conn.Open();

                bool hasImageColumn = InventoryDatabaseHelper.ProductImageColumnExists();

                string query = hasImageColumn
                    ? @"
                    UPDATE Products SET
                        product_name=@n,
                        description=@d,
                        category_id=@c,
                        unit_id=@u,
                        reorder_point=@rp,
                        SellingPrice=@sp,
                        active=@a,
                        image_path=@img,
                        product_image=@productImage
                    WHERE ProductID=@id"
                    : @"
                    UPDATE Products SET
                        product_name=@n,
                        description=@d,
                        category_id=@c,
                        unit_id=@u,
                        reorder_point=@rp,
                        SellingPrice=@sp,
                        active=@a,
                        image_path=@img
                    WHERE ProductID=@id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@n", newName);
                    cmd.Parameters.AddWithValue("@d", description);
                    cmd.Parameters.AddWithValue("@c", categoryId);
                    cmd.Parameters.AddWithValue("@u", unitId);
                    cmd.Parameters.AddWithValue("@rp", reorderPoint);
                    cmd.Parameters.AddWithValue("@sp", sellingPrice);
                    cmd.Parameters.AddWithValue("@a", active);
                    cmd.Parameters.AddWithValue("@img", imageFileName);
                    if (hasImageColumn)
                    {
                        cmd.Parameters.AddWithValue("@productImage", (object)selectedImageBytes ?? DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("@id", currentProductId);

                    cmd.ExecuteNonQuery();
                }
            }

            ImageService.ClearCache();

            TryLogInventoryUpdate(
                currentProductId,
                originalProductName,
                originalImagePath,
                originalImageBytes,
                newName,
                imageFilePath,
                selectedImageBytes);

            MessageBox.Show("Product updated successfully!", "Success");

            OnProductUpdated?.Invoke(this, EventArgs.Empty);
            CloseEditForm();
        }


        private void CloseEditForm()
        {
            if (pcbBlurOverlay != null)
                pcbBlurOverlay.Visible = false;

            // Dispose the scroll container that holds this form
            if (ParentScrollContainer != null)
            {
                if (ParentScrollContainer.Parent != null)
                    ParentScrollContainer.Parent.Controls.Remove(ParentScrollContainer);

                ParentScrollContainer.Dispose();
                ParentScrollContainer = null;
            }

            this.Dispose();


        }

        private void ImageUploadBox_Click(object sender, EventArgs e)
        {
            string suggestedName = !string.IsNullOrWhiteSpace(SKUtxtbox.Text)
                ? SKUtxtbox.Text.Trim()
                : ProductNametxtbox.Text.Trim();

            if (hasProductImageColumn)
            {
                if (ImageService.TrySelectImageBytes(ImageCategory.Product, suggestedName, out byte[] imageBytes, out string savedPath, out string originalPath))
                {
                    imageFilePath = savedPath;
                    selectedImageBytes = imageBytes;
                    ImageUploadBox.Text = Path.GetFileName(savedPath);
                    UpdateImagePreview(selectedImageBytes, imageFilePath);
                }
            }
            else if (ImageService.TrySelectAndSaveImage(ImageCategory.Product, suggestedName, out string savedPath, out string originalPath))
            {
                imageFilePath = savedPath;
                selectedImageBytes = ImageService.ConvertImageToBytes(ImageService.GetImage(savedPath, ImageCategory.Product));
                ImageUploadBox.Text = Path.GetFileName(savedPath);
                UpdateImagePreview(selectedImageBytes, imageFilePath);
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            CloseEditForm();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            CloseEditForm();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            UpdateProduct();
        }

        private void UpdateImagePreview(byte[] imageBytes, string imagePath = null)
        {
            if (imagePreviewBox == null)
            {
                return;
            }

            if (imageBytes != null && imageBytes.Length > 0)
            {
                imagePreviewBox.Image = ImageService.ConvertBytesToImage(imageBytes);
            }
            else if (!string.IsNullOrWhiteSpace(imagePath))
            {
                imagePreviewBox.Image = ImageService.GetImage(imagePath, ImageCategory.Product);
            }
            else
            {
                imagePreviewBox.Image = ImageService.GetPlaceholderImage();
            }
        }

        private void TryLogInventoryUpdate(
            string productId,
            string oldName,
            string oldImagePath,
            byte[] oldImageBytes,
            string newName,
            string newImagePath,
            byte[] newImageBytes)
        {
            try
            {
                string oldValues = $"Name={oldName}; ImagePath={oldImagePath}; HasImageBytes={(oldImageBytes != null)}";
                string newValues = $"Name={newName}; ImagePath={newImagePath}; HasImageBytes={(newImageBytes != null)}";

                AuditHelper.LogWithDetails(
                    AuditModule.INVENTORY,
                    $"Updated product {newName}",
                    AuditActivityType.UPDATE,
                    tableAffected: "Products",
                    recordId: productId,
                    oldValues: oldValues,
                    newValues: newValues
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Inventory audit log failed: {ex.Message}");
            }
        }
    }
}
