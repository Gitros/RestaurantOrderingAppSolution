using Domain;

namespace Application.Dtos.OrderItems;

public class OrderItemReadDto
{
    public Guid Id { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string SpecialInstructions { get; set; }

    public Guid OrderId { get; set; }
    public Guid MenuItemId { get; set; }

    public string MenuItemName { get; set; }
    public string OrderItemStatus { get; set; }
}
