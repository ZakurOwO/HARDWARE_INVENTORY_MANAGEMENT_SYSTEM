using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using System.Data.SqlClient;
using System.Data;
using System.IO;

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
        using (SqlConnection connection = new SqlConnection(ConnectionString.DataSource))
        {
            connection.Open();
            string query = @"INSERT INTO Products 
                        (product_name, description, category_id, unit_id, 
                         current_stock, image_path, reorder_point, active) 
                        VALUES 
                        (@productName, @description, @categoryId, @unitId, 
                         @currentStock, @imagePath, @reorderPoint, @active)";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@productName", productName);
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