using Application.Dtos.MenuTypes;

namespace Application.Contracts;

public interface IMenuTypeService
{
    Task<MenuTypeReadDto> CreateMenuType(MenuTypeCreateDto menuTypeCreateDto);
    Task<MenuTypeReadDto> UpdateMenuType(MenuTypeUpdateDto menuTypeUpdateDto, Guid id);
    Task DeleteMenuType(Guid id);
    Task<MenuTypeReadDto> GetMenuType(Guid id);
    Task<List<MenuTypeReadDto>> GetAllMenuTypes();
}
