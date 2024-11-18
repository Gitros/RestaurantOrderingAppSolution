using Application.Contracts;
using Application.Dtos.MenuTypes;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class MenuTypeController : BaseApiController
{
    private readonly IMenuTypeService _menuTypeService;

    public MenuTypeController(IMenuTypeService menuTypeService)
    {
        _menuTypeService = menuTypeService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateMenuType([FromBody] MenuTypeCreateDto menuTypeCreateDto) =>
        HandleResult(await _menuTypeService.CreateMenuType(menuTypeCreateDto));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMenuType(Guid id) =>
        HandleResult(await _menuTypeService.GetMenuType(id));

    [HttpGet]
    public async Task<IActionResult> GetAllMenuTypes() =>
        HandleResult(await _menuTypeService.GetAllMenuTypes());

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMenuType([FromBody] MenuTypeUpdateDto menuTypeUpdateDto, Guid id) =>
        HandleResult(await _menuTypeService.UpdateMenuType(menuTypeUpdateDto, id));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMenuType(Guid id) =>
        HandleResult(await _menuTypeService.DeleteMenuType(id));
}
