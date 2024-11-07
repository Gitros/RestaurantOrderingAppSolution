using Application.Dtos.Common;
using Application.Dtos.MenuTypes;

namespace Application.Contracts;

public interface IMenuTypeService
{
    Task<ResultDto<MenuTypeReadDto>> CreateMenuType(MenuTypeCreateDto menuTypeCreateDto);
    Task<ResultDto<MenuTypeReadDto>> GetMenuType(Guid id);
    Task<ResultDto<List<MenuTypeReadDto>>> GetAllMenuTypes();
    Task<ResultDto<MenuTypeReadDto>> UpdateMenuType(MenuTypeUpdateDto menuTypeUpdateDto, Guid id);
    Task<ResultDto<bool>> DeleteMenuType(Guid id);
}
