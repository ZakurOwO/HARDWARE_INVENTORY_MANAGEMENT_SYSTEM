using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

public static class InventoryDatabaseHelper
{
    public static DataTable LoadCategoriesFromDatabase()
    {
        DataTable dt = new DataTable();
        using (SqlConnection connection = new SqlConnection(ConnectionString.DataSource))
        {
            connection.Open();
            string query = "SELECT CategoryID, category_name FROM Categories ORDER BY category_name";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
        }
        return dt;
    }

    public static DataTable LoadUnitsFromDatabase()
    {
        DataTable dt = new DataTable();
        using (SqlConnection connection = new SqlConnection(ConnectionString.DataSource))
        {
            connection.Open();
            string query = "SELECT UnitID, unit_name FROM Units ORDER BY unit_name";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
        }
        return dt;
    }

    public static bool AddProduct(string productName, string description, string categoryId,
                              string unitId, int currentStock, string imageFileName,
                              int reorderPoint, bool active)
    {
        // Generate SKU automatically
        string sku = GenerateSKU(productName, categoryId);

        using (SqlConnection connection = new SqlConnection(ConnectionString.DataSource))
        {
            connection.Open();
            string query = @"INSERT INTO Products 
                        (product_name, SKU, description, category_id, unit_id, 
                         current_stock, image_path, reorder_point, active) 
                        VALUES 
                        (@productName, @sku, @description, @categoryId, @unitId, 
                         @currentStock, @imagePath, @reorderPoint, @active)";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@productName", productName);
                cmd.Parameters.AddWithValue("@sku", sku);
                cmd.Parameters.AddWithValue("@description", description ?? "");
                cmd.Parameters.AddWithValue("@categoryId", categoryId);
                cmd.Parameters.AddWithValue("@unitId", unitId);
                cmd.Parameters.AddWithValue("@currentStock", currentStock);
                cmd.Parameters.AddWithValue("@imagePath", imageFileName ?? "");
                cmd.Parameters.AddWithValue("@reorderPoint", reorderPoint);
                cmd.Parameters.AddWithValue("@active", active);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }

    private static string GenerateSKU(string productName, string categoryId)
    {
        // Get category prefix from database
        string categoryPrefix = GetCategoryPrefix(categoryId);

        // Generate a unique number based on timestamp
        string timestamp = DateTime.Now.ToString("yyMMddHHmmss");

        // Take first 3 letters of product name (uppercase, remove spaces)
        string productCode = productName.Length >= 3
            ? productName.Substring(0, 3).ToUpper().Replace(" ", "")
            : productName.PadRight(3, 'X').ToUpper().Replace(" ", "");

        return $"{categoryPrefix}-{productCode}-{timestamp.Substring(6)}";
    }

    private static string GetCategoryPrefix(string categoryId)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionString.DataSource))
        {
            connection.Open();
            string query = "SELECT category_name FROM Categories WHERE CategoryID = @categoryId";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@categoryId", categoryId);
                string categoryName = cmd.ExecuteScalar()?.ToString() ?? "GEN";

                // Generate prefix from category name
                if (categoryName.Length >= 3)
                    return categoryName.Substring(0, 3).ToUpper();
                else
                    return categoryName.PadRight(3, 'X').ToUpper();
            }
        }
    }

    public static bool UpdateProductStock(string productName, int newStock)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionString.DataSource))
        {
            connection.Open();
            string query = "UPDATE Products SET current_stock = @newStock WHERE product_name = @productName";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@newStock", newStock);
                cmd.Parameters.AddWithValue("@productName", productName);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }

    public static bool IsProductNameExists(string productName)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionString.DataSource))
        {
            connection.Open();
            string query = "SELECT COUNT(*) FROM Products WHERE product_name = @productName";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@productName", productName);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
    }
}