using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
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

        // 🔥 Added overlay reference
        private PictureBox pcbBlurOverlay;

        public event EventHandler OnProductUpdated;

        // 🔥 Updated constructor to accept blur overlay
        public EditItem_Form(string productId, PictureBox blurOverlay)
        {
            InitializeComponent();
            currentProductId = productId;
            pcbBlurOverlay = blurOverlay;

            // Show overlay when form opens
            if (pcbBlurOverlay != null)
                pcbBlurOverlay.Visible = true;

            LoadCategoriesAndUnits();
            LoadProductData();
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

            
        }

        // ============================= PICK IMAGE =============================
        

        // ============================= UPDATE PRODUCT =============================
        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            UpdateProduct();
        }

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

            string imageFileName = ImageUploadBox.Text.Trim();

            if (!string.IsNullOrEmpty(imageFilePath))
            {
                string folder = Path.Combine(Application.StartupPath, "ImageInventory");
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string dest = Path.Combine(folder, Path.GetFileName(imageFilePath));
                File.Copy(imageFilePath, dest, true);

                imageFileName = Path.GetFileName(imageFilePath);
            }

            using (SqlConnection conn = new SqlConnection(ConnectionString.DataSource))
            {
                conn.Open();

                string query = @"
                    UPDATE Products SET
                        product_name=@n,
                        description=@d,
                        category_id=@c,
                        unit_id=@u,
                        current_stock=@s,
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
                    cmd.Parameters.AddWithValue("@id", currentProductId);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Product updated successfully!", "Success");

            OnProductUpdated?.Invoke(this, EventArgs.Empty);
            CloseEditForm();
        }

        // ============================= CLOSE FORM =============================
        private void CloseBtn_Click(object sender, EventArgs e)
        {
            CloseEditForm();
        }

        private void CloseEditForm()
        {
            // Hide overlay
            if (pcbBlurOverlay != null)
                pcbBlurOverlay.Visible = false;

            this.Dispose();
        }

        private void ImageUploadBox_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Images|*.png;*.jpg;*.jpeg;*.bmp";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    imageFilePath = ofd.FileName;
                    ImageUploadBox.Text = Path.GetFileName(ofd.FileName);

                    // No preview
                }
            }
        }
    }
}
