using Application.Contracts;
using Application.Dtos.MenuItems;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuItemController : ControllerBase
{
    private readonly IMenuItemService _menuItemService;

    public MenuItemController(IMenuItemService menuItemService)
    {
        _menuItemService = menuItemService;
    }

    [HttpPost]
    public async Task<MenuItemReadDto> CreateMenuItem([FromBody]MenuItemCreateDto menuItemCreateDto) => 
        await _menuItemService.CreateMenuItem(menuItemCreateDto);

    [HttpGet("{id}")]
    public async Task<MenuItemReadDto> GetMenuItem(Guid id) =>
        await _menuItemService.GetMenuItem(id);

    [HttpGet]
    public async Task<List<MenuItemReadDto>> GetAllMenuItem() =>
        await _menuItemService.GetAllMenuItems();

    [HttpPut("{id}")]
    public async Task<MenuItemReadDto> UpdateMenuItem(MenuItemUpdateDto menuItemUpdateDto, Guid id) =>
        await _menuItemService.UpdateMenuItem(menuItemUpdateDto, id);

    [HttpDelete("{id}")]
    public async Task DeleteMenuItem(Guid id) =>
        await _menuItemService.DeleteMenuItem(id);
}
