using System;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Models
{
    public class CartItem
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int ProductInternalID { get; set; } // This line is crucial

        // Optional: Calculated property
        public decimal TotalPrice
        {
            get { return Price * Quantity; }
        }
    }
}