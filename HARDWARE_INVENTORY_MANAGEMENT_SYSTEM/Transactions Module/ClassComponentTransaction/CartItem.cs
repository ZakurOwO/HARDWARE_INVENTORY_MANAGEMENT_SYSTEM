using System;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Models
{
    /// <summary>
    /// Helper class to represent cart items
    /// </summary>
    public class CartItem
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total => Quantity * Price;
        public int ProductInternalID { get; set; }

        // Additional useful properties
        public string ProductID { get; set; }
        public string ImagePath { get; set; }
        public int AvailableStock { get; set; }

        // Validation method
        public bool IsValid()
        {
            return Quantity > 0 &&
                   Quantity <= AvailableStock &&
                   Price >= 0 &&
                   !string.IsNullOrEmpty(ProductName);
        }
    }
}