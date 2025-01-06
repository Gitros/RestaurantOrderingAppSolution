using Application.Dtos.OrderItemIngredients;

namespace Application.Dtos.OrderItems;

public class OrderItemSummaryDto
{
    public Guid Id { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string? SpecialInstructions { get; set; }
    public string MenuItemName { get; set; } = null!;
    public string OrderItemStatus { get; set; } = null!;

    public List<OrderItemIngredientReadDto> Ingredients { get; set; } = new List<OrderItemIngredientReadDto>();
}
