using System;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Models
{
    /// <summary>
    /// Unified cart item model aligned with database schema
    /// </summary>
    public class CartItem
    {
        public int ProductInternalID { get; set; }
        public string ProductID { get; set; }
        public string SKU { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }

        /// <summary>
        /// Unit selling price mapped to Products.SellingPrice
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Backwards-compatible alias for UnitPrice used throughout the UI
        /// </summary>
        public decimal Price
        {
            get => UnitPrice;
            set => UnitPrice = value;
        }

        public decimal TotalPrice => UnitPrice * Quantity;
    }
}