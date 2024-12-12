namespace Application.Dtos.MenuItems;

public class MenuItemCreateDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public bool IsUsed { get; set; }

    public Guid MenuCategoryId { get; set; }
    public List<Guid> TagIds { get; set; } = new List<Guid>();
}
