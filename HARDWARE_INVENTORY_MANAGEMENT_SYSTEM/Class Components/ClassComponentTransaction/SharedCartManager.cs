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

        public event EventHandler CartUpdated;
        public event EventHandler InventoryUpdated;

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

            var existingItem = _cartItems.Find(x => x.ProductInternalID == item.ProductInternalID);
            int existingQuantity = existingItem != null ? existingItem.Quantity : 0;
            int desiredQuantity = existingQuantity + item.Quantity;

            if (!TryGetCurrentStock(item.ProductInternalID, out int availableStock))
            {
                return false;
            }

            if (availableStock <= 0)
            {
                MessageBox.Show("Item is out of stock.", "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (desiredQuantity > availableStock)
            {
                MessageBox.Show($"Insufficient stock. Available: {availableStock}", "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
                existingItem.Price = item.Price;
                existingItem.ProductName = item.ProductName;
                existingItem.ProductID = item.ProductID;
                existingItem.ImagePath = item.ImagePath;
                existingItem.AvailableStock = availableStock;
            }
            else
            {
                item.AvailableStock = availableStock;
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
            return RemoveItemAndRestoreStock(productInternalId);
        }

        public bool RemoveItemAndRestoreStock(int productInternalId, int? quantityOverride = null)
        {
            var existingItem = _cartItems.Find(x => x.ProductInternalID == productInternalId);
            if (existingItem == null)
            {
                return false;
            }

            int removedQuantity = quantityOverride.HasValue && quantityOverride.Value > 0
                ? quantityOverride.Value
                : existingItem.Quantity;

            _cartItems.RemoveAll(x => x.ProductInternalID == productInternalId);

            LogCartAction(
                "Removed item from cart",
                productInternalId.ToString(),
                $"{{\"removed_quantity\":{removedQuantity}}}"
            );

            CartUpdated?.Invoke(this, EventArgs.Empty);
            InventoryUpdated?.Invoke(this, EventArgs.Empty);

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
                if (!TryGetCurrentStock(productInternalId, out int availableStock))
                {
                    return false;
                }

                if (newQuantity > availableStock)
                {
                    MessageBox.Show($"Insufficient stock. Available: {availableStock}", "Stock Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
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
            // restoreStock is ignored because cart changes are now purely in-memory until checkout

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

        public void RaiseCartUpdated()
        {
            CartUpdated?.Invoke(this, EventArgs.Empty);
        }

        public void RaiseInventoryUpdated()
        {
            InventoryUpdated?.Invoke(this, EventArgs.Empty);
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

        /// <summary>
        /// Apply the current cart quantities to inventory within the provided database transaction.
        /// This is the only place where product stock levels are modified.
        /// </summary>
        public bool ApplyCartToInventory(SqlConnection connection, SqlTransaction transaction, out string errorMessage)
        {
            errorMessage = null;

            foreach (var item in _cartItems)
            {
                string selectQuery = "SELECT current_stock FROM Products WITH (UPDLOCK, HOLDLOCK) WHERE ProductInternalID = @ProductId";
                using (SqlCommand selectCmd = new SqlCommand(selectQuery, connection, transaction))
                {
                    selectCmd.Parameters.AddWithValue("@ProductId", item.ProductInternalID);
                    object result = selectCmd.ExecuteScalar();
                    if (result == null)
                    {
                        errorMessage = "Product not found in inventory.";
                        return false;
                    }

                    int currentStock = Convert.ToInt32(result);
                    if (currentStock < item.Quantity)
                    {
                        errorMessage = $"Insufficient stock for {item.ProductName}. Available: {currentStock}";
                        return false;
                    }
                }

                string updateQuery = "UPDATE Products SET current_stock = current_stock - @QuantityChange WHERE ProductInternalID = @ProductId";
                using (SqlCommand updateCmd = new SqlCommand(updateQuery, connection, transaction))
                {
                    updateCmd.Parameters.AddWithValue("@QuantityChange", item.Quantity);
                    updateCmd.Parameters.AddWithValue("@ProductId", item.ProductInternalID);

                    int rows = updateCmd.ExecuteNonQuery();
                    if (rows == 0)
                    {
                        errorMessage = "Unable to update stock for one or more products.";
                        return false;
                    }
                }
            }

            return true;
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
            CartUpdated?.Invoke(this, EventArgs.Empty);
            InventoryUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}
