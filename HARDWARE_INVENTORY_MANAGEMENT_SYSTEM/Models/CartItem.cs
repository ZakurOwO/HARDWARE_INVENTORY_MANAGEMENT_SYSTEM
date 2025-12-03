using System;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Models
{
    public class CartItem
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int ProductInternalID { get; set; } // This line is crucial
        public string ProductID { get; set; }
        public string ImagePath { get; set; }
        public int AvailableStock { get; set; }

        // Optional: Calculated property
        public decimal TotalPrice
        {
            get { return Price * Quantity; }
        }

        public bool IsValid()
        {
            return ProductInternalID > 0
                && Quantity > 0
                && Price >= 0;
        }
    }
}