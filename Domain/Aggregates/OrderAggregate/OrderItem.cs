using Domain.Aggregates.MenuAggregate;

namespace Domain.Aggregates.OrderAggregate;

public class OrderItem
{
    public int OrderItemId { get; set; }
    public Guid OrderId { get; set; }
    public int MenuItemId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string SpecialInstructions { get; set; }

    public Order Order { get; set; }
    public MenuItem MenuItem { get; set; }
}
