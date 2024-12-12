namespace Application.Dtos.Ingredients;

public class IngredientCreateDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int IngredientType { get; set; }
}
