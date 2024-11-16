namespace Application.Dtos.OrderItems;

public class OrderItemCreateDto
{
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string SpecialInstructions { get; set; }

    public Guid MenuItemId { get; set; }
    public Guid OrderId { get; set; }
}
