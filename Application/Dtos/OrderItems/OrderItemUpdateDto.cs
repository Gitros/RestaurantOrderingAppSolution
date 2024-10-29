namespace Application.Dtos.OrderItems;

public class OrderItemUpdateDto
{
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string SpecialInstructions { get; set; }

    public Guid MenuItemId { get; set; }
}
