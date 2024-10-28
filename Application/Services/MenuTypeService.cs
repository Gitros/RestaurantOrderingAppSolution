using Application.Contracts;
using Application.Dtos.MenuTypes;
using Domain;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

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

    public async Task DeleteMenuType(Guid id)
    {
        var menuTypeToDelete = await _orderingContext.Menus.FindAsync(id);

        if (menuTypeToDelete != null)
        {
            _orderingContext.Menus.Remove(menuTypeToDelete);
            await _orderingContext.SaveChangesAsync();
        }
    }

    public async Task<List<MenuTypeReadDto>> GetAllMenuTypes()
    {
        var menuTypes = await _orderingContext.Menus
            .Select(menu => new MenuTypeReadDto
            {
                Id = Guid.NewGuid(),
                Name = menu.Name,
            })
            .ToListAsync();

        return menuTypes;
    }

    public async Task<MenuTypeReadDto> GetMenuType(Guid id)
    {
        var menuType = await _orderingContext.Menus.FirstOrDefaultAsync(x => x.Id == id);

        return new MenuTypeReadDto
        {
            Id = id,
            Name = menuType.Name,
        };
    }

    public Task<MenuTypeReadDto> UpdateMenuType(MenuTypeUpdateDto menuTypeUpdateDto, Guid id)
    {
        throw new NotImplementedException();
    }
}
