using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Models;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Data
{
    public class ProductDataAccess
    {
        private readonly string connectionString;

        public ProductDataAccess()
        {
            connectionString = ConnectionString.DataSource;
        }

        public List<Product> GetActiveProducts()
        {
            var products = new List<Product>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT 
                            p.ProductInternalID,
                            p.ProductID,
                            p.product_name,
                            p.SKU,
                            p.description,
                            p.category_id,
                            p.unit_id,
                            p.current_stock,
                            p.image_path,
                            pb.cost_per_unit,  -- Changed from selling_price to cost_per_unit
                            p.active
                        FROM Products p
                        LEFT JOIN ProductBatches pb ON p.ProductInternalID = pb.product_id
                        WHERE p.active = 1
                        ORDER BY p.product_name";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var product = new Product
                                {
                                    ProductInternalID = reader.GetInt32(reader.GetOrdinal("ProductInternalID")),
                                    ProductID = reader["ProductID"].ToString(),
                                    ProductName = reader["product_name"].ToString(),
                                    SKU = reader["SKU"].ToString(),
                                    Description = reader["description"].ToString(),
                                    CategoryID = reader["category_id"].ToString(),
                                    UnitID = reader["unit_id"].ToString(),
                                    CurrentStock = reader.GetInt32(reader.GetOrdinal("current_stock")),
                                    ImagePath = reader["image_path"].ToString(),
                                    SellingPrice = reader["cost_per_unit"] != DBNull.Value ?
                                                 Convert.ToDecimal(reader["cost_per_unit"]) : 0,
                                    Active = Convert.ToBoolean(reader["active"])
                                };
                                products.Add(product);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return products;
        }

        public List<Product> SearchProducts(string searchTerm)
        {
            var products = new List<Product>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT 
                            p.ProductInternalID,
                            p.ProductID,
                            p.product_name,
                            p.SKU,
                            p.description,
                            p.category_id,
                            p.unit_id,
                            p.current_stock,
                            p.image_path,
                            pb.cost_per_unit,  -- Changed from selling_price to cost_per_unit
                            p.active
                        FROM Products p
                        LEFT JOIN ProductBatches pb ON p.ProductInternalID = pb.product_id
                        WHERE p.active = 1 
                        AND (p.product_name LIKE @searchTerm OR p.SKU LIKE @searchTerm)
                        ORDER BY p.product_name";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@searchTerm", $"%{searchTerm}%");

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var product = new Product
                                {
                                    ProductInternalID = reader.GetInt32(reader.GetOrdinal("ProductInternalID")),
                                    ProductID = reader["ProductID"].ToString(),
                                    ProductName = reader["product_name"].ToString(),
                                    SKU = reader["SKU"].ToString(),
                                    Description = reader["description"].ToString(),
                                    CategoryID = reader["category_id"].ToString(),
                                    UnitID = reader["unit_id"].ToString(),
                                    CurrentStock = reader.GetInt32(reader.GetOrdinal("current_stock")),
                                    ImagePath = reader["image_path"].ToString(),
                                    SellingPrice = reader["cost_per_unit"] != DBNull.Value ?
                                                 Convert.ToDecimal(reader["cost_per_unit"]) : 0,
                                    Active = Convert.ToBoolean(reader["active"])
                                };
                                products.Add(product);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching products: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return products;
        }
    }
}