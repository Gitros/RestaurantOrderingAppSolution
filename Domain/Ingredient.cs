namespace Domain;

public class Ingredient
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public Guid MenuItemId { get; set; }
    public MenuItem MenuItem { get; set; }
}
