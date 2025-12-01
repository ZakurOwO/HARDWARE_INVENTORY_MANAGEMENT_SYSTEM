using System;
using System.Data;
using System.Data.SqlClient;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components.Class_Compnents_Of_Inventory
{
    public class ProductDetailData
    {
        public string ProductId { get; set; }
        public int ProductInternalId { get; set; }
        public string ProductName { get; set; }
        public string SKU { get; set; }
        public string Category { get; set; }
        public string Unit { get; set; }
        public int CurrentStock { get; set; }
        public int ReorderPoint { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal CostPrice { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string Status { get; set; }
        public DateTime? OrderedDate { get; set; }
        public DateTime? TransitDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public DateTime? AvailableDate { get; set; }
        public string SupplierName { get; set; }
        public string LastBatchNumber { get; set; }
        public string LastDeliveryNumber { get; set; }
    }

    public static DataTable LoadCategoriesFromDatabase()
    {
        public class ProductDetailData
        {
            public string ProductId { get; set; }
            public int ProductInternalId { get; set; }
            public string ProductName { get; set; }
            public string SKU { get; set; }
            public string Category { get; set; }
            public string Unit { get; set; }
            public int CurrentStock { get; set; }
            public int ReorderPoint { get; set; }
            public decimal SellingPrice { get; set; }
            public decimal CostPrice { get; set; }
            public string Description { get; set; }
            public string ImagePath { get; set; }
            public string Status { get; set; }
            public DateTime? OrderedDate { get; set; }
            public DateTime? TransitDate { get; set; }
            public DateTime? ReceivedDate { get; set; }
            public DateTime? AvailableDate { get; set; }
            public string SupplierName { get; set; }
            public string LastBatchNumber { get; set; }
            public string LastDeliveryNumber { get; set; }
        }

        public static DataTable LoadCategoriesFromDatabase()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConnectionString.DataSource))
            {
                connection.Open();
                const string query = "SELECT CategoryID, category_name FROM Categories ORDER BY category_name";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
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
                const string query = "SELECT UnitID, unit_name FROM Units ORDER BY unit_name";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
            return dt;
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

        public static bool AddProduct(
            string productName,
            string description,
            string categoryId,
            string unitId,
            int currentStock,
            string imageFileName,
            int reorderPoint,
            bool active,
            out string productId,
            out string sku)
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
                    cmd.Parameters.AddWithValue("@description", description ?? string.Empty);
                    cmd.Parameters.AddWithValue("@categoryId", categoryId);
                    cmd.Parameters.AddWithValue("@unitId", unitId);
                    cmd.Parameters.AddWithValue("@currentStock", currentStock);
                    cmd.Parameters.AddWithValue("@imagePath", imageFileName ?? string.Empty);
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
            string categoryPrefix = GetCategoryPrefix(categoryId);
            string timestamp = DateTime.Now.ToString("yyMMddHHmmss");
            string productCode = productName.Length >= 3
                ? productName.Substring(0, 3).ToUpper().Replace(" ", string.Empty)
                : productName.PadRight(3, 'X').ToUpper().Replace(" ", string.Empty);

            return $"{categoryPrefix}-{productCode}-{timestamp.Substring(6)}";
        }

        private static string GetCategoryPrefix(string categoryId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.DataSource))
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

        public static bool UpdateProductStock(string productName, int newStock)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.DataSource))
            {
                connection.Open();
                const string query = "UPDATE Products SET current_stock = @newStock WHERE product_name = @productName";

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
                const string query = "SELECT COUNT(*) FROM Products WHERE product_name = @productName";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@productName", productName);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        public static ProductDetailData GetProductDetails(string productId)
        {
            ProductDetailData detail = null;

            using (SqlConnection connection = new SqlConnection(ConnectionString.DataSource))
            {
                connection.Open();

                const string baseQuery = @"
                SELECT TOP 1
                    p.ProductInternalID,
                    p.ProductID,
                    p.product_name,
                    p.SKU,
                    c.category_name,
                    u.unit_name,
                    p.current_stock,
                    p.reorder_point,
                    p.SellingPrice,
                    p.description,
                    p.image_path,
                    CASE WHEN p.active = 1 THEN 'Active' ELSE 'Inactive' END AS status,
                    p.created_at,
                    p.updated_at
                FROM Products p
                INNER JOIN Categories c ON p.category_id = c.CategoryID
                INNER JOIN Units u ON p.unit_id = u.UnitID
                WHERE p.ProductID = @productId";

                using (SqlCommand cmd = new SqlCommand(baseQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@productId", productId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            detail = new ProductDetailData
                            {
                                ProductInternalId = reader.GetInt32(reader.GetOrdinal("ProductInternalID")),
                                ProductId = reader["ProductID"].ToString(),
                                ProductName = reader["product_name"].ToString(),
                                SKU = reader["SKU"].ToString(),
                                Category = reader["category_name"].ToString(),
                                Unit = reader["unit_name"].ToString(),
                                CurrentStock = Convert.ToInt32(reader["current_stock"]),
                                ReorderPoint = Convert.ToInt32(reader["reorder_point"]),
                                SellingPrice = reader["SellingPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["SellingPrice"]),
                                Description = reader["description"].ToString(),
                                ImagePath = reader["image_path"].ToString(),
                                Status = reader["status"].ToString(),
                                AvailableDate = reader["updated_at"] as DateTime? ?? DateTime.Now,
                                OrderedDate = reader["created_at"] as DateTime? ?? DateTime.Now,
                            };
                        }
                    }
                }

                if (detail == null)
                    return null;

                const string batchQuery = @"
                SELECT TOP 1 pb.cost_per_unit, pb.batch_number, po.po_date, po.po_number, s.supplier_name
                FROM ProductBatches pb
                LEFT JOIN PurchaseOrders po ON pb.po_id = po.po_id
                LEFT JOIN Suppliers s ON po.supplier_id = s.supplier_id
                WHERE pb.product_id = @productInternalId
                ORDER BY pb.received_date DESC";

                using (SqlCommand cmd = new SqlCommand(batchQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@productInternalId", detail.ProductInternalId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            detail.CostPrice = reader["cost_per_unit"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["cost_per_unit"]);
                            detail.LastBatchNumber = reader["batch_number"].ToString();
                            detail.SupplierName = reader["supplier_name"].ToString();
                            detail.OrderedDate = reader["po_date"] as DateTime? ?? detail.OrderedDate;
                        }
                    }
                }

                const string deliveryQuery = @"
                SELECT TOP 1 d.delivery_date, d.DeliveryID
                FROM DeliveryItems di
                INNER JOIN Deliveries d ON di.delivery_id = d.delivery_id
                WHERE di.product_id = @productInternalId
                ORDER BY d.delivery_date DESC";

                using (SqlCommand cmd = new SqlCommand(deliveryQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@productInternalId", detail.ProductInternalId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            detail.TransitDate = reader["delivery_date"] as DateTime? ?? detail.TransitDate;
                            detail.ReceivedDate = detail.TransitDate;
                            detail.LastDeliveryNumber = reader["DeliveryID"].ToString();
                        }
                    }
                }

                if (!detail.TransitDate.HasValue || !detail.ReceivedDate.HasValue)
                {
                    const string auditQuery = @"
                    SELECT TOP 1 timestamp FROM AuditLog
                    WHERE record_id = @productId OR record_id = @sku
                    ORDER BY timestamp DESC";

                    using (SqlCommand cmd = new SqlCommand(auditQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@productId", (object)detail.ProductId ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@sku", (object)detail.SKU ?? DBNull.Value);
                        var value = cmd.ExecuteScalar();
                        if (value != null && value != DBNull.Value)
                        {
                            DateTime ts = Convert.ToDateTime(value);
                            detail.TransitDate ??= ts;
                            detail.ReceivedDate ??= ts;
                            detail.AvailableDate ??= ts;
                        }
                    }
                }
            }

            return detail;
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

                const string query = @"
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
