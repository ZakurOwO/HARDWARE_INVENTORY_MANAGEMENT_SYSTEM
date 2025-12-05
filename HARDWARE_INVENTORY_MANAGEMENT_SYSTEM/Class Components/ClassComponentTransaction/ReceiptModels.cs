using System;
using System.Collections.Generic;

namespace HARDWARE_INVENTORY_MANAGEMENT_SYSTEM.Transactions_Module
{
    public sealed class ReceiptItem
    {
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get { return UnitPrice * Quantity; } }
    }

    public sealed class ReceiptData
    {
        public string StoreName { get; set; }
        public string StoreSubtitle { get; set; }
        public string DocumentTitle { get; set; }

        public string TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }     // "Delivery Order" / "Walk-in"
        public string PaymentMethod { get; set; }       // "Check" etc.

        // These MUST be from checkout (accuracy guarantee)
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }

        public List<ReceiptItem> Items { get; set; }

        public ReceiptData()
        {
            StoreName = "Topaz Hardware";
            StoreSubtitle = "Hardware & Construction Supplies";
            DocumentTitle = "Receipt";
            TransactionDate = DateTime.Now;
            TransactionType = "Delivery Order";
            PaymentMethod = "Check";
            Items = new List<ReceiptItem>();
        }
    }
}
