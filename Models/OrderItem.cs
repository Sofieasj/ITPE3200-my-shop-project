namespace MyShop.Models;

public class OrderItem
{
    public int OrderItemId {get; set; }
    public int ItemId {get; set; }
    //navigation property
    public Item Item {get; set; } = default!;
    public int Quantity {get; set; }
    public int OrderId {get; set; }
    // navigation property
    public Order Order {get; set; } = default!;
    public decimal OrderItemPrice {get; set; }
}