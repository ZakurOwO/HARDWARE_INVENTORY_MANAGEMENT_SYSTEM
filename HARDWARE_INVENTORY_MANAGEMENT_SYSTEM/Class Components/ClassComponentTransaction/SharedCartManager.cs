using System;
using System.Collections.Generic;
using HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Models;

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
                return;
            }

            // Check if item already exists by internal ID for consistency
            var existingItem = _cartItems.Find(x => x.ProductInternalID == item.ProductInternalID);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
                existingItem.Price = item.Price; // keep price in sync with latest selection
                existingItem.ProductName = item.ProductName;
            }
            else
            {
                _cartItems.Add(item);
            }
        }

        public void RemoveItemFromCart(int productInternalId)
        {
            _cartItems.RemoveAll(x => x.ProductInternalID == productInternalId);
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
            }
        }

        public void ClearCart()
        {
            _cartItems.Clear();
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
    }
}