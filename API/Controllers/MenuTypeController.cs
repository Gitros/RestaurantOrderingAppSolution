using Application.Contracts;
using Application.Dtos.MenuTypes;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class MenuTypeController : ControllerBase
{
    private IMenuTypeService _menuTypeService;

    public MenuTypeController(IMenuTypeService menuTypeService)
    {
        _menuTypeService = menuTypeService;
    }

    [HttpPost]
    public async Task<MenuTypeReadDto> CreateMenuType(MenuTypeCreateDto menuTypeCreateDto) => 
        await _menuTypeService.CreateMenuType(menuTypeCreateDto);

    [HttpGet]
    public async Task<MenuTypeReadDto> GetMenuType(Guid id) => 
        await _menuTypeService.GetMenuType(id);

    [HttpDelete]
    public async Task DeleteMenuType(Guid id) => 
        await _menuTypeService.DeleteMenuType(id);
}
