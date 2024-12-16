using Application.Contracts;
using Application.Dtos.MenuItems;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class MenuItemController : BaseApiController
{
    private readonly IMenuItemService _menuItemService;

    public MenuItemController(IMenuItemService menuItemService)
    {
        _menuItemService = menuItemService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateMenuItem([FromBody] MenuItemCreateDto menuItemCreateDto) =>
        HandleResult(await _menuItemService.CreateMenuItem(menuItemCreateDto));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMenuItem(Guid id) =>
        HandleResult(await _menuItemService.GetMenuItem(id));

    [HttpGet]
    public async Task<IActionResult> GetAllMenuItems() =>
        HandleResult(await _menuItemService.GetAllMenuItems());

    [HttpGet("category/{categoryId}")]
    public async Task<IActionResult> GetMenuItemsByCategory(Guid categoryId) =>
    HandleResult(await _menuItemService.GetMenuItemsByCategory(categoryId));


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMenuItem([FromBody] MenuItemUpdateDto menuItemUpdateDto, Guid id) =>
        HandleResult(await _menuItemService.UpdateMenuItem(menuItemUpdateDto, id));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMenuItem(Guid id) =>
        HandleResult(await _menuItemService.DeleteMenuItem(id));
}
