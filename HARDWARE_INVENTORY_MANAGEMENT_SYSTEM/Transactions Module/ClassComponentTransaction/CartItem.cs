/// <summary>
/// Helper class to represent cart items
/// </summary>
public class CartItem
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Total => Quantity * Price; // Add this line
    public int ProductInternalID { get; set; }
}