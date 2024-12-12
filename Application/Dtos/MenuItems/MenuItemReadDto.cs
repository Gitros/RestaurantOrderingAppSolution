using Application.Dtos.MenuItemTags;

namespace Application.Dtos.MenuItems;

public class MenuItemReadDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public bool IsUsed { get; set; }
    public List<MenuItemTagReadDto> Tags { get; set; } = new List<MenuItemTagReadDto>();
}
