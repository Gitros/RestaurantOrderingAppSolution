using Application.Dtos.OrderItemIngredients;
using Domain;

namespace Application.Dtos.OrderItems;

public class OrderItemUpdateDto
{
    public int Quantity { get; set; }
    public string SpecialInstructions { get; set; }

    public List<OrderItemIngredientAddDto> Ingredients { get; set; } = new List<OrderItemIngredientAddDto>();
}
