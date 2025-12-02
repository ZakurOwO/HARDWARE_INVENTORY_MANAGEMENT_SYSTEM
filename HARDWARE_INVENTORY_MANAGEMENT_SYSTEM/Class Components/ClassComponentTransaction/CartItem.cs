using System;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Models
{
    /// <summary>
    /// Represents a single item in the shopping cart.
    /// </summary>
    public class CartItem
    {
        // This must exist
        public int ProductInternalID { get; set; }

        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string ImagePath { get; set; }

        public int Quantity { get; set; }
        public int AvailableStock { get; set; }

        public decimal Price { get; set; }
        public decimal Total => Quantity * Price;

        public bool IsValid()
        {
            return ProductInternalID > 0 &&
                   Quantity > 0 &&
                   Price >= 0 &&
                   !string.IsNullOrWhiteSpace(ProductName);
        }


    }
}
