using Application.Contracts;
using Application.Dtos.MenuTypes;
using Domain;
using Infrastructure.Database;

namespace Application.Services;

public class MenuTypeService : IMenuTypeService
{
    private RestaurantOrderingContext _orderingContext;

    public MenuTypeService(RestaurantOrderingContext orderingContext)
    {
        _orderingContext = orderingContext;
    }

    public async Task<MenuTypeReadDto> CreateMenuType(MenuTypeCreateDto menuTypeCreateDto)
    {
        var menuType = new MenuType
        {
            Id = Guid.NewGuid(),
            Name = menuTypeCreateDto.Name,
        };

        var result = await _orderingContext.Menus.AddAsync(menuType);
        await _orderingContext.SaveChangesAsync();

        var createdMenuType = new MenuTypeReadDto
        {
            Id = result.Entity.Id,
            Name = result.Entity.Name
        };
        
        return createdMenuType;
    }

    public Task DeleteMenuType(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<MenuTypeReadDto>> GetAllMenuTypes()
    {
        throw new NotImplementedException();
    }

    public Task<MenuTypeReadDto> GetMenuType(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<MenuTypeReadDto> UpdateMenuType(MenuTypeUpdateDto menuTypeUpdateDto, Guid id)
    {
        throw new NotImplementedException();
    }
}
