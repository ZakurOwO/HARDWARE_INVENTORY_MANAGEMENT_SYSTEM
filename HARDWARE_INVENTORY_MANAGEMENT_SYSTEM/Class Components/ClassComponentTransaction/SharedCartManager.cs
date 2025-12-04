using System;
using System.Collections.Generic;
using System.Linq;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Class_Components.ClassComponentTransaction
{
    /// <summary>
    /// Centralized in-memory cart manager shared across transaction forms.
    /// No database updates occur here; persistence happens only during checkout.
    /// </summary>
    public class SharedCartManager
    {
        private static SharedCartManager _instance;
        private readonly Dictionary<int, CartItem> _cartItems = new Dictionary<int, CartItem>();
        private readonly Dictionary<int, int> _originalQuantities = new Dictionary<int, int>();

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
        }

        public event EventHandler CartUpdated;
        public event EventHandler InventoryUpdated;

        public class CartItem
        {
            public int ProductInternalId { get; set; }
            public int ProductInternalID { get => ProductInternalId; set => ProductInternalId = value; }
            public string ProductId { get; set; }
            public string ProductID { get => ProductId; set => ProductId = value; }
            public string Name { get; set; }
            public string ProductName { get => Name; set => Name = value; }
            public string Sku { get; set; }
            public string SKU { get => Sku; set => Sku = value; }
            public decimal UnitPrice { get; set; }
            public decimal Price { get => UnitPrice; set => UnitPrice = value; }
            public int Quantity { get; set; }
            public int AvailableStock { get; set; }
            public decimal LineTotal => UnitPrice * Quantity;
        }

        public void AddOrUpdateItem(CartItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (!_originalQuantities.ContainsKey(item.ProductInternalId))
            {
                _originalQuantities[item.ProductInternalId] = 0;
            }

            _cartItems[item.ProductInternalId] = CloneItem(item);
            RaiseCartUpdated();
            RaiseInventoryUpdated();
        }

        public void RemoveItem(int productInternalId)
        {
            if (_cartItems.Remove(productInternalId))
            {
                RaiseCartUpdated();
                RaiseInventoryUpdated();
            }
        }

        public int GetItemQuantity(int productInternalId)
        {
            return _cartItems.TryGetValue(productInternalId, out var item) ? item.Quantity : 0;
        }

        public IReadOnlyList<CartItem> GetItems()
        {
            return _cartItems.Values.Select(CloneItem).ToList();
        }

        public void ClearCart()
        {
            _cartItems.Clear();
            _originalQuantities.Clear();
            RaiseCartUpdated();
            RaiseInventoryUpdated();
        }

        public void RemoveItemAndRestoreStock(int productInternalId)
        {
            RemoveItem(productInternalId);
        }

        public void RaiseCartUpdated()
        {
            CartUpdated?.Invoke(this, EventArgs.Empty);
        }

        public void RaiseInventoryUpdated()
        {
            InventoryUpdated?.Invoke(this, EventArgs.Empty);
        }

        // Compatibility helpers for existing UI code -------------------------
        public bool AddItemToCart(CartItem item)
        {
            var newItem = CloneItem(item);
            int existingQuantity = GetItemQuantity(newItem.ProductInternalId);
            newItem.Quantity = existingQuantity + newItem.Quantity;
            AddOrUpdateItem(newItem);
            return true;
        }

        public bool UpdateItemQuantity(int productInternalId, int newQuantity)
        {
            if (newQuantity <= 0)
            {
                RemoveItem(productInternalId);
                return true;
            }

            if (_cartItems.TryGetValue(productInternalId, out var existing))
            {
                var updated = CloneItem(existing);
                updated.Quantity = newQuantity;
                AddOrUpdateItem(updated);
                return true;
            }

            return false;
        }

        public bool RemoveItemFromCart(int productInternalId)
        {
            if (!_cartItems.ContainsKey(productInternalId))
            {
                return false;
            }

            RemoveItem(productInternalId);
            return true;
        }

        public int GetCartItemCount()
        {
            return _cartItems.Count;
        }

        public decimal CalculateSubtotal()
        {
            return _cartItems.Values.Sum(i => i.UnitPrice * i.Quantity);
        }
        // --------------------------------------------------------------------

        private CartItem CloneItem(CartItem item)
        {
            return new CartItem
            {
                ProductInternalId = item.ProductInternalId,
                ProductId = item.ProductId,
                Name = item.Name,
                Sku = item.Sku,
                UnitPrice = item.UnitPrice,
                Quantity = item.Quantity,
                AvailableStock = item.AvailableStock
            };
        }
    }
}
