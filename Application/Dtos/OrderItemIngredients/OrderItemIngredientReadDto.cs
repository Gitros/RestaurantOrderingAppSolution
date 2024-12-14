namespace Application.Dtos.OrderItemIngredients;

public class OrderItemIngredientReadDto
{
    public Guid IngredientId { get; set; }
    public string IngredientName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
