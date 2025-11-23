// In Models/Product.cs
namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Models
{
    public class Product
    {
        public int ProductInternalID { get; set; }
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        public string CategoryID { get; set; }
        public string UnitID { get; set; }
        public int CurrentStock { get; set; }
        public string ImagePath { get; set; }
        public decimal SellingPrice { get; set; }
        public bool Active { get; set; }

        // Navigation properties
        public string CategoryName { get; set; }
        public string UnitName { get; set; }
    }
}