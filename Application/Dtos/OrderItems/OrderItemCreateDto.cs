using Application.Dtos.OrderItemIngredients;

namespace Application.Dtos.OrderItems;

public class OrderItemCreateDto
{
    public int Quantity { get; set; }
    public string SpecialInstructions { get; set; }

    public Guid MenuItemId { get; set; }
}
