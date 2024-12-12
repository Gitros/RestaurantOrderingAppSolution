namespace Domain;

public class OrderItemIngredient
{
    public Guid OrderItemId { get; set; }
    public OrderItem OrderItem { get; set; }

    public Guid IngredientId { get; set; }
    public Ingredient Ingredient { get; set; }

    public int Quantity { get; set; }
}
