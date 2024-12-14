using Application.Dtos.OrderItemIngredients;

namespace Application.Dtos.OrderItems;

public class OrderItemCreateDto
{
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string SpecialInstructions { get; set; }

    public List<OrderItemIngredientAddDto> Ingredients { get; set; } = new List<OrderItemIngredientAddDto>();

    public Guid MenuItemId { get; set; }
}
