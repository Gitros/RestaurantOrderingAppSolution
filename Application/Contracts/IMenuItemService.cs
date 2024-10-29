using Application.Dtos.MenuItems;

namespace Application.Contracts;

public interface IMenuItemService
{
    Task<MenuItemReadDto> CreateMenuItem(MenuItemCreateDto menuItemCreateDto);
    Task<MenuItemReadDto> GetMenuItem(Guid id);
    Task<List<MenuItemReadDto>> GetAllMenuItems();
    Task<MenuItemReadDto> UpdateMenuItem(MenuItemUpdateDto menuItemUpdateDto, Guid id);
    Task DeleteMenuItem(Guid id);
}
