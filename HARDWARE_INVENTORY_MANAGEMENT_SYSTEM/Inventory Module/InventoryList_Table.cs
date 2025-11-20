using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Inventory_Module
{
    public partial class InventoryList_Table : UserControl
    {
        public InventoryList_Table()
        {
            InitializeComponent();
        }

        private void InventoryList_Table_Load(object sender, EventArgs e)
        {
            LoadDataFromDatabase();
        }

        public void RefreshData()
        {
            LoadDataFromDatabase();
        }

        private void LoadDataFromDatabase()
        {
            try
            {
                dgvInventoryList.Rows.Clear();

                using (SqlConnection connection = new SqlConnection(ConnectionString.DataSource))
                {
                    connection.Open();
                    string query = @"
                SELECT 
                    p.product_name,
                    c.category_name,
                    p.current_stock,
                    p.reorder_point,
                    CASE WHEN p.active = 1 THEN 'Active' ELSE 'Inactive' END as status,
                    p.image_path
                FROM Products p
                INNER JOIN Categories c ON p.category_id = c.CategoryID
                ORDER BY p.product_name";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string productName = reader["product_name"].ToString();
                                string category = reader["category_name"].ToString();
                                int currentStock = Convert.ToInt32(reader["current_stock"]);
                                int reorderPoint = Convert.ToInt32(reader["reorder_point"]);
                                string status = reader["status"].ToString();
                                string imagePath = reader["image_path"].ToString();

                                // SIMPLE IMAGE LOADING
                                Image productImage = ProductImageManager.GetProductImage(imagePath);

                                // Load action icons
                                Image adjustStockIcon = Properties.Resources.AdjustStock;
                                Image deactivateIcon = Properties.Resources.Deactivate_Circle1;
                                Image viewDetailsIcon = Properties.Resources.Group_10481;

                                dgvInventoryList.Rows.Add(
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
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Clean up only product images (column 1), not resource icons
                foreach (DataGridViewRow row in dgvInventoryList.Rows)
                {
                    if (!row.IsNewRow && row.Cells[1].Value is Image image)
                    {
                        if (!IsDefaultImage(image))
                        {
                            image.Dispose();
                        }
                    }
                }
            }
            base.Dispose(disposing);
        }
    }
}