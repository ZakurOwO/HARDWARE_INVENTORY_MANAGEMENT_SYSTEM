using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Models;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components.ClassComponentTransaction
{
    /// <summary>
    /// Singleton that holds cart items for the current session.
    /// </summary>
    public class SharedCartManager
    {
        private static SharedCartManager _instance;
        private readonly List<CartItem> _cartItems;
        private readonly string connectionString;

        public event Action CartUpdated;
        public event Action InventoryUpdated;

        public static SharedCartManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SharedCartManager();
                }
                return _instance;
            }
        }

        private SharedCartManager()
        {
            _cartItems = new List<CartItem>();
            connectionString = ConnectionString.DataSource;
        }

        public List<CartItem> GetCartItems()
        {
            return new List<CartItem>(_cartItems);
        }

        public bool AddItemToCart(CartItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (!item.IsValid())
                throw new ArgumentException("Cart item is not valid.", nameof(item));

            int additionalQuantity = item.Quantity;
            var existingItem = _cartItems.Find(x => x.ProductInternalID == item.ProductInternalID);

            if (!TryGetCurrentStock(item.ProductInternalID, out int availableStock))
            {
                return false;
            }

            if (availableStock <= 0)
            {
                MessageBox.Show("Item is out of stock.", "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (additionalQuantity > availableStock)
            {
                MessageBox.Show($"Insufficient stock. Available: {availableStock}", "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!TryAdjustStock(item.ProductInternalID, additionalQuantity, out string errorMessage))
            {
                MessageBox.Show(errorMessage, "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
                existingItem.Price = item.Price;
                existingItem.ProductName = item.ProductName;
                existingItem.ProductID = item.ProductID;
                existingItem.ImagePath = item.ImagePath;
                existingItem.AvailableStock = item.AvailableStock;
            }
            else
            {
                _cartItems.Add(item);
            }

            LogCartAction(
                "Added item to cart",
                item.ProductInternalID.ToString(),
                $"{{\"product_id\":{item.ProductInternalID},\"quantity\":{item.Quantity},\"price\":{item.Price}}}"
            );

            NotifyCartAndInventoryChanged();

            return true;
        }

        public bool RemoveItemFromCart(int productInternalId)
        {
            var existingItem = _cartItems.Find(x => x.ProductInternalID == productInternalId);
            if (existingItem == null)
            {
                return false;
            }

            if (!TryAdjustStock(productInternalId, -existingItem.Quantity, out string errorMessage))
            {
                MessageBox.Show(errorMessage, "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            _cartItems.RemoveAll(x => x.ProductInternalID == productInternalId);

            LogCartAction(
                "Removed item from cart",
                productInternalId.ToString(),
                null
            );

            NotifyCartAndInventoryChanged();

            return true;
        }

        public bool UpdateItemQuantity(int productInternalId, int newQuantity)
        {
            var existingItem = _cartItems.Find(x => x.ProductInternalID == productInternalId);
            if (existingItem == null)
                return false;

            if (newQuantity <= 0)
            {
                return RemoveItemFromCart(productInternalId);
            }
            else
            {
                int quantityChange = newQuantity - existingItem.Quantity;
                if (quantityChange != 0)
                {
                    if (!TryAdjustStock(productInternalId, quantityChange, out string errorMessage))
                    {
                        MessageBox.Show(errorMessage, "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }

                existingItem.Quantity = newQuantity;

                LogCartAction(
                    "Updated cart item quantity",
                    productInternalId.ToString(),
                    $"{{\"quantity\":{newQuantity}}}"
                );

                NotifyCartAndInventoryChanged();
            }

            return true;
        }

        public void ClearCart(bool restoreStock)
        {
            if (restoreStock)
            {
                foreach (var item in new List<CartItem>(_cartItems))
                {
                    if (!TryAdjustStock(item.ProductInternalID, -item.Quantity, out string errorMessage))
                    {
                        MessageBox.Show(errorMessage, "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }

            _cartItems.Clear();
            LogCartAction("Cleared cart", null, null);

            NotifyCartAndInventoryChanged();
        }

        public int GetCartItemCount()
        {
            return _cartItems.Count;
        }

        public int GetItemQuantity(int productInternalId)
        {
            var existingItem = _cartItems.Find(x => x.ProductInternalID == productInternalId);
            return existingItem != null ? existingItem.Quantity : 0;
        }

        public decimal CalculateSubtotal()
        {
            decimal subtotal = 0;
            foreach (var item in _cartItems)
            {
                subtotal += item.Quantity * item.Price;
            }
            return subtotal;
        }

        private void LogCartAction(string activity, string recordId, string newValues)
        {
            try
            {
                AuditHelper.LogWithDetails(
                    AuditModule.SALES,
                    activity,
                    AuditActivityType.UPDATE,
                    "Cart",
                    recordId,
                    null,
                    newValues
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Audit log failed for cart action: {ex.Message}");
            }
        }

        private bool TryAdjustStock(int productId, int quantityChange, out string errorMessage)
        {
            errorMessage = null;

            if (quantityChange == 0)
            {
                return true;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        string selectQuery = "SELECT current_stock FROM Products WHERE ProductInternalID = @ProductId";
                        using (SqlCommand selectCmd = new SqlCommand(selectQuery, connection, transaction))
                        {
                            selectCmd.Parameters.AddWithValue("@ProductId", productId);
                            object result = selectCmd.ExecuteScalar();
                            if (result == null)
                            {
                                errorMessage = "Product not found in inventory.";
                                transaction.Rollback();
                                return false;
                            }

                            int currentStock = Convert.ToInt32(result);
                            if (currentStock <= 0 && quantityChange > 0)
                            {
                                errorMessage = "Item is out of stock.";
                                transaction.Rollback();
                                return false;
                            }

                            int projectedStock = currentStock - quantityChange;

                            if (projectedStock < 0)
                            {
                                errorMessage = $"Insufficient stock. Available: {currentStock}";
                                transaction.Rollback();
                                return false;
                            }

                            string updateQuery = "UPDATE Products SET current_stock = current_stock - @QuantityChange WHERE ProductInternalID = @ProductId";
                            using (SqlCommand updateCmd = new SqlCommand(updateQuery, connection, transaction))
                            {
                                updateCmd.Parameters.AddWithValue("@QuantityChange", quantityChange);
                                updateCmd.Parameters.AddWithValue("@ProductId", productId);
                                int rows = updateCmd.ExecuteNonQuery();

                                if (rows == 0)
                                {
                                    errorMessage = "Unable to update stock for the selected product.";
                                    transaction.Rollback();
                                    return false;
                                }
                            }

                            transaction.Commit();
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Error updating stock: {ex.Message}";
                return false;
            }
        }

        private bool TryGetCurrentStock(int productId, out int currentStock)
        {
            currentStock = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string selectQuery = "SELECT current_stock FROM Products WHERE ProductInternalID = @ProductId";
                    using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@ProductId", productId);
                        object result = cmd.ExecuteScalar();
                        if (result == null)
                        {
                            MessageBox.Show("Product not found in inventory.", "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }

                        currentStock = Convert.ToInt32(result);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking stock: {ex.Message}", "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void NotifyCartAndInventoryChanged()
        {
            CartUpdated?.Invoke();
            InventoryUpdated?.Invoke();
        }
    }
}
