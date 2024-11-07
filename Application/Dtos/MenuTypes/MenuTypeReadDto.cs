using Application.Dtos.MenuItems;
using Domain;

namespace Application.Dtos.MenuTypes;

public class MenuTypeReadDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsUsed { get; set; }
    public bool IsDeleted { get; set; }
    public List<MenuItemReadDto> MenuItems { get; set; } = new List<MenuItemReadDto>();
}
