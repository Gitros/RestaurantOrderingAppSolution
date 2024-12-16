using Application.Contracts;
using Application.Dtos.MenuCategories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class MenuCategoryController : BaseApiController
{
    private readonly IMenuCategoryService _menuCategoryService;

    public MenuCategoryController(IMenuCategoryService menuCategoryService)
    {
        _menuCategoryService = menuCategoryService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateMenuCategory([FromBody] MenuCategoryCreateDto menuCategoryCreateDto) =>
        HandleResult(await _menuCategoryService.CreateMenuCategory(menuCategoryCreateDto));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMenuCategory(Guid id) =>
        HandleResult(await _menuCategoryService.GetMenuCategory(id));

    [HttpGet("menu-categories")]
    public async Task<IActionResult> GetAllMenuCategories() =>
        HandleResult(await _menuCategoryService.GetAllMenuCategories());

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMenuCategory([FromBody] MenuCategoryUpdateDto menuCategoryUpdateDto, Guid id) =>
        HandleResult(await _menuCategoryService.UpdateMenuCategory(menuCategoryUpdateDto, id));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMenuCategory(Guid id) =>
        HandleResult(await _menuCategoryService.DeleteMenuCategory(id));
}
