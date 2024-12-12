using Application.Dtos.MenuItems;

namespace Application.Dtos.MenuCategories;

public class MenuCategoryReadDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsUsed { get; set; }
    public bool IsDeleted { get; set; }

    public List<MenuItemReadDto> MenuItems { get; set; } = new List<MenuItemReadDto>();
}
