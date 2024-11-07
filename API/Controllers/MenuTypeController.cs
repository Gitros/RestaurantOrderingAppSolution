using Application.Contracts;
using Application.Dtos.Common;
using Application.Dtos.MenuTypes;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuTypeController : ControllerBase
{
    private readonly IMenuTypeService _menuTypeService;

    public MenuTypeController(IMenuTypeService menuTypeService)
    {
        _menuTypeService = menuTypeService;
    }

    [HttpPost]
    public async Task<ResultDto<MenuTypeReadDto>> CreateMenuType([FromBody]MenuTypeCreateDto menuTypeCreateDto) => 
        await _menuTypeService.CreateMenuType(menuTypeCreateDto);

    [HttpGet("{id}")]
    public async Task<ResultDto<MenuTypeReadDto>> GetMenuType(Guid id) => 
        await _menuTypeService.GetMenuType(id);

    [HttpGet]
    public async Task<ResultDto<List<MenuTypeReadDto>>> GetAllMenuTypes() => 
        await _menuTypeService.GetAllMenuTypes();

    [HttpPut("{id}")]
    public async Task<ResultDto<MenuTypeReadDto>> UpdateMenuType(MenuTypeUpdateDto menuTypeUpdateDto, Guid id) => 
        await _menuTypeService.UpdateMenuType(menuTypeUpdateDto, id);

    [HttpDelete("{id}")]
    public async Task<ResultDto<bool>> DeleteMenuType(Guid id) => 
        await _menuTypeService.DeleteMenuType(id);
}
