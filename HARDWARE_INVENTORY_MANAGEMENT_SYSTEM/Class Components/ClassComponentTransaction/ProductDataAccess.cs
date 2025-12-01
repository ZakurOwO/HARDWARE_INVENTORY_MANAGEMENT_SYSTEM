using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Models;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Data
{
    public class ProductDataAccess
    {
        private string connectionString;

        public ProductDataAccess()
        {
            connectionString = ConnectionString.DataSource;
        }

        public List<Product> GetActiveProducts()
        {
            var products = new List<Product>();
            Console.WriteLine("🔍 Fetching active products from database...");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        p.ProductInternalID,
                        p.ProductID,
                        p.product_name as ProductName,
                        p.SKU,
                        p.description as Description,
                        p.category_id as CategoryID,
                        p.unit_id as UnitID,
                        p.current_stock as CurrentStock,
                        p.image_path as ImagePath,
                        ISNULL(p.SellingPrice, 0.00) as SellingPrice,
                        p.active as Active,
                        c.category_name as CategoryName,
                        u.unit_name as UnitName
                    FROM Products p
                    LEFT JOIN Categories c ON p.category_id = c.CategoryID
                    LEFT JOIN Units u ON p.unit_id = u.UnitID
                    WHERE p.active = 1 AND p.current_stock > 0
                    ORDER BY p.product_name";

                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    int count = 0;
                    while (reader.Read())
                    {
                        count++;
                        var product = new Product
                        {
                            ProductInternalID = reader.GetInt32(0),
                            ProductID = reader.GetString(1),
                            ProductName = reader.GetString(2),
                            SKU = reader.GetString(3),
                            Description = reader.IsDBNull(4) ? "" : reader.GetString(4),
                            CategoryID = reader.GetString(5),
                            UnitID = reader.GetString(6),
                            CurrentStock = reader.GetInt32(7),
                            ImagePath = reader.IsDBNull(8) ? "" : reader.GetString(8),
                            SellingPrice = reader.GetDecimal(9),
                            Active = reader.GetBoolean(10),
                            CategoryName = reader.IsDBNull(11) ? "" : reader.GetString(11),
                            UnitName = reader.IsDBNull(12) ? "" : reader.GetString(12)
                        };

                        products.Add(product);
                        Console.WriteLine($"  - {product.ProductName} (Stock: {product.CurrentStock}, Price: {product.SellingPrice})");
                    }
                    reader.Close();

                    Console.WriteLine($"✅ Successfully loaded {count} active products");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error retrieving products: {ex.Message}");
                    throw new Exception($"Error retrieving products: {ex.Message}");
                }
            }

            return products;
        }

        public List<Product> SearchProducts(string searchTerm)
        {
            var products = new List<Product>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        p.ProductInternalID,
                        p.ProductID,
                        p.product_name as ProductName,
                        p.SKU,
                        p.description as Description,
                        p.category_id as CategoryID,
                        p.unit_id as UnitID,
                        p.current_stock as CurrentStock,
                        p.image_path as ImagePath,
                        ISNULL(p.SellingPrice, 0.00) as SellingPrice,
                        p.active as Active,
                        c.category_name as CategoryName,
                        u.unit_name as UnitName
                    FROM Products p
                    LEFT JOIN Categories c ON p.category_id = c.CategoryID
                    LEFT JOIN Units u ON p.unit_id = u.UnitID
                    WHERE p.active = 1 
                    AND (p.product_name LIKE @SearchTerm 
                         OR p.SKU LIKE @SearchTerm 
                         OR p.description LIKE @SearchTerm)
                    ORDER BY p.product_name";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            ProductInternalID = reader.GetInt32(0),
                            ProductID = reader.GetString(1),
                            ProductName = reader.GetString(2),
                            SKU = reader.GetString(3),
                            Description = reader.IsDBNull(4) ? "" : reader.GetString(4),
                            CategoryID = reader.GetString(5),
                            UnitID = reader.GetString(6),
                            CurrentStock = reader.GetInt32(7),
                            ImagePath = reader.IsDBNull(8) ? "" : reader.GetString(8),
                            SellingPrice = reader.GetDecimal(9),
                            Active = reader.GetBoolean(10),
                            CategoryName = reader.IsDBNull(11) ? "" : reader.GetString(11),
                            UnitName = reader.IsDBNull(12) ? "" : reader.GetString(12)
                        });
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error searching products: {ex.Message}");
                }
            }

            return products;
        }

        public Product GetProductById(int productInternalId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT 
                        p.ProductInternalID,
                        p.ProductID,
                        p.product_name as ProductName,
                        p.SKU,
                        p.description as Description,
                        p.category_id as CategoryID,
                        p.unit_id as UnitID,
                        p.current_stock as CurrentStock,
                        p.image_path as ImagePath,
                        ISNULL(p.SellingPrice, 0.00) as SellingPrice,
                        p.active as Active
                    FROM Products p
                    WHERE p.ProductInternalID = @ProductInternalID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductInternalID", productInternalId);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        return new Product
                        {
                            ProductInternalID = reader.GetInt32(0),
                            ProductID = reader.GetString(1),
                            ProductName = reader.GetString(2),
                            SKU = reader.GetString(3),
                            Description = reader.IsDBNull(4) ? "" : reader.GetString(4),
                            CategoryID = reader.GetString(5),
                            UnitID = reader.GetString(6),
                            CurrentStock = reader.GetInt32(7),
                            ImagePath = reader.IsDBNull(8) ? "" : reader.GetString(8),
                            SellingPrice = reader.GetDecimal(9),
                            Active = reader.GetBoolean(10)
                        };
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error retrieving product: {ex.Message}");
                }
            }

            return null;
        }

        public bool CheckSellingPriceColumnExists()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT COUNT(*) 
                    FROM INFORMATION_SCHEMA.COLUMNS 
                    WHERE TABLE_NAME = 'Products' 
                    AND COLUMN_NAME = 'SellingPrice'";

                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error checking column existence: {ex.Message}");
                }
            }
        }
    }
}