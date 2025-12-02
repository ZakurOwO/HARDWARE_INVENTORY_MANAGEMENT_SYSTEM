using System;
using System.Collections.Generic;
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
                              int reorderPoint, bool active, out string productId, out string sku)
    {
        // Generate SKU automatically
        sku = GenerateSKU(productName, categoryId);
        productId = null;

        using (SqlConnection connection = new SqlConnection(ConnectionString.DataSource))
        {
            connection.Open();
            string query = @"INSERT INTO Products
                        (product_name, SKU, description, category_id, unit_id,
                         current_stock, image_path, reorder_point, active)
                        OUTPUT inserted.ProductID
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

                var result = cmd.ExecuteScalar();
                productId = result?.ToString();

                return !string.IsNullOrWhiteSpace(productId);
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

    public static DataTable GetProductHistory(string productId, string sku, string productName)
    {
        DataTable historyTable = new DataTable();
        historyTable.Columns.Add("Direction");
        historyTable.Columns.Add("QuantityChange");
        historyTable.Columns.Add("Reference");
        historyTable.Columns.Add("Timestamp", typeof(DateTime));

        using (SqlConnection connection = new SqlConnection(ConnectionString.DataSource))
        {
            connection.Open();

            string query = @"
                SELECT TOP 50 activity, activity_type, record_id, timestamp, new_values, old_values, module
                FROM AuditLog
                WHERE
                    (record_id = @productId OR record_id = @sku OR record_id = @productName)
                    AND (table_affected IS NULL OR table_affected IN ('Products', 'ProductBatches'))
                ORDER BY timestamp DESC";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@productId", (object)productId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@sku", (object)sku ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@productName", (object)productName ?? DBNull.Value);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string activityType = reader["activity_type"].ToString();
                        string newValues = reader["new_values"]?.ToString();
                        string oldValues = reader["old_values"]?.ToString();

                        int? oldStock = ParseStockValue(oldValues);
                        int? newStock = ParseStockValue(newValues);

                        string direction = string.Empty;
                        string quantityChange = string.Empty;

                        if (oldStock.HasValue && newStock.HasValue)
                        {
                            int delta = newStock.Value - oldStock.Value;
                            direction = delta >= 0 ? "IN" : "OUT";
                            quantityChange = Math.Abs(delta).ToString();
                        }
                        else if (string.Equals(activityType, "CREATE", StringComparison.OrdinalIgnoreCase))
                        {
                            direction = "IN";
                        }

                        historyTable.Rows.Add(
                            direction,
                            quantityChange,
                            reader["record_id"]?.ToString(),
                            reader.GetDateTime(reader.GetOrdinal("timestamp"))
                        );
                    }
                }
            }
        }

        return historyTable;
    }

    public static InventoryProductDetails GetProductDetails(string productId)
    {
        if (string.IsNullOrWhiteSpace(productId))
        {
            return null;
        }

        using (SqlConnection connection = new SqlConnection(ConnectionString.DataSource))
        {
            connection.Open();

            string query = @"
                SELECT
                    p.ProductID,
                    p.product_name,
                    p.SKU,
                    p.description,
                    c.category_name,
                    u.unit_name,
                    p.current_stock,
                    p.reorder_point,
                    ISNULL(p.SellingPrice, 0.00) AS selling_price,
                    p.active,
                    p.image_path
                FROM Products p
                LEFT JOIN Categories c ON p.category_id = c.CategoryID
                LEFT JOIN Units u ON p.unit_id = u.UnitID
                WHERE p.ProductID = @productId";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@productId", productId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new InventoryProductDetails
                        {
                            ProductId = reader["ProductID"]?.ToString(),
                            ProductName = reader["product_name"]?.ToString(),
                            SKU = reader["SKU"]?.ToString(),
                            Description = reader["description"]?.ToString(),
                            CategoryName = reader["category_name"]?.ToString(),
                            UnitName = reader["unit_name"]?.ToString(),
                            CurrentStock = reader["current_stock"] as int? ?? Convert.ToInt32(reader["current_stock"]),
                            ReorderPoint = reader["reorder_point"] as int? ?? Convert.ToInt32(reader["reorder_point"]),
                            SellingPrice = reader["selling_price"] as decimal? ?? Convert.ToDecimal(reader["selling_price"]),
                            Active = reader["active"] is bool active ? active : Convert.ToBoolean(reader["active"]),
                            ImagePath = reader["image_path"]?.ToString()
                        };
                    }
                }
            }
        }

        return null;
    }

    public static List<DateTime> GetRecentActivityDates(string productId, string sku, string productName, int maxEntries = 4)
    {
        var history = GetProductHistory(productId, sku, productName);
        var timeline = new List<DateTime>();

        foreach (DataRow row in history.Rows)
        {
            if (row["Timestamp"] is DateTime timestamp)
            {
                timeline.Add(timestamp);
            }

            if (timeline.Count >= maxEntries)
            {
                break;
            }
        }

        timeline.Sort();
        return timeline;
    }

    private static int? ParseStockValue(string values)
    {
        if (string.IsNullOrWhiteSpace(values))
            return null;

        string[] parts = values.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string part in parts)
        {
            if (part.Trim().StartsWith("Stock=", StringComparison.OrdinalIgnoreCase))
            {
                string number = part.Split('=')[1].Trim();
                if (int.TryParse(number, out int stock))
                    return stock;
            }
        }
        return null;
    }
}

public class InventoryProductDetails
{
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string SKU { get; set; }
    public string Description { get; set; }
    public string CategoryName { get; set; }
    public string UnitName { get; set; }
    public int CurrentStock { get; set; }
    public int ReorderPoint { get; set; }
    public decimal SellingPrice { get; set; }
    public bool Active { get; set; }
    public string ImagePath { get; set; }
}