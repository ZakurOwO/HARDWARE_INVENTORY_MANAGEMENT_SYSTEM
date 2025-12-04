using System;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Models
{
    /// <summary>
    /// Represents a single item in the shopping cart.
    /// </summary>
    public class CartItem
    {
        // This must exist
        public int ProductInternalId { get; set; }
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string Sku { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int AvailableStock { get; set; }

        public decimal LineTotal => UnitPrice * Quantity;

        // Backward-compat aliases (for old code)
        public int ProductInternalID { get => ProductInternalId; set => ProductInternalId = value; }
        public string ProductID { get => ProductId; set => ProductId = value; }
        public string ProductName { get => Name; set => Name = value; }
        public string SKU { get => Sku; set => Sku = value; }
        public decimal Price { get => UnitPrice; set => UnitPrice = value; }

        public bool IsValid()
        {
            return ProductInternalID > 0 &&
                   Quantity > 0 &&
                   Price >= 0 &&
                   !string.IsNullOrWhiteSpace(ProductName);
        }


    }
}
