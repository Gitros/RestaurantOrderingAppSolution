namespace Domain;

public class Ingredient
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public bool IsUsed { get; set; }
    public bool IsDeleted { get; set; }

    public List<OrderItemIngredient> OrderItemIngredients { get; set; } = new List<OrderItemIngredient>();
    public IngredientType IngredientType { get; set; }
}

public enum IngredientType
{
    Cheese = 0,
    Vegetables = 1,
    Meat = 2,
    Other = 3
}