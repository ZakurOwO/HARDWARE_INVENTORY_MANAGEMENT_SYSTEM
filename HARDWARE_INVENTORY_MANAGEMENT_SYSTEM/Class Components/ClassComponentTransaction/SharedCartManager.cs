using System;
using System.Collections.Generic;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Models;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Audit_Log;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module
{
    public class SharedCartManager
    {
        private static SharedCartManager _instance;
        private List<CartItem> _cartItems;

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
        }

        public List<CartItem> GetCartItems()
        {
            return new List<CartItem>(_cartItems);
        }

        public void AddItemToCart(CartItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (item.ProductInternalID <= 0)
            {
                throw new ArgumentException("A valid ProductInternalID is required to add an item to the cart.", nameof(item));
            }

            var existingItem = _cartItems.Find(x => x.ProductInternalID == item.ProductInternalID);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
                existingItem.Price = item.Price; // ensure latest price is stored
                existingItem.ProductName = item.ProductName;
            }
            else
            {
                _cartItems.Add(item);
            }

            LogCartAction("Added item to cart", item.ProductInternalID.ToString(),
                $"{{\"product_id\":{item.ProductInternalID},\"quantity\":{item.Quantity},\"price\":{item.Price}}}");
        }

        public void RemoveItemFromCart(int productInternalId)
        {
            _cartItems.RemoveAll(x => x.ProductInternalID == productInternalId);

            LogCartAction("Removed item from cart", productInternalId.ToString(), null);
        }

        public void UpdateItemQuantity(int productInternalId, int newQuantity)
        {
            var existingItem = _cartItems.Find(x => x.ProductInternalID == productInternalId);
            if (existingItem != null)
            {
                if (newQuantity <= 0)
                {
                    RemoveItemFromCart(productInternalId);
                }
                else
                {
                    existingItem.Quantity = newQuantity;
                }

                LogCartAction("Updated cart item quantity", productInternalId.ToString(),
                    $"{{\"quantity\":{newQuantity}}}");
            }
        }

        public void ClearCart()
        {
            _cartItems.Clear();

            LogCartAction("Cleared cart", null, null);
        }

        public int GetCartItemCount()
        {
            return _cartItems.Count;
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
                    newValues);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Audit log failed for cart action: {ex.Message}");
            }
        }
    }
}