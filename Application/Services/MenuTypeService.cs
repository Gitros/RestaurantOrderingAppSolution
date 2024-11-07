using Application.Contracts;
using Application.Dtos.MenuTypes;
using AutoMapper;
using Domain;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class MenuTypeService : IMenuTypeService
{
    private RestaurantOrderingContext _orderingContext;
    private readonly IMapper _mapper;

    public MenuTypeService(RestaurantOrderingContext orderingContext, IMapper mapper)
    {
        _orderingContext = orderingContext;
        _mapper = mapper;
    }

    public async Task<MenuTypeReadDto> CreateMenuType(MenuTypeCreateDto menuTypeCreateDto)
    {
        var menuType = new MenuType
        {
            Id = Guid.NewGuid(),
            Name = menuTypeCreateDto.Name,
        };

        var result = await _orderingContext.MenuTypes.AddAsync(menuType);
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
        var menuTypeToDelete = await _orderingContext.MenuTypes.FindAsync(id);

        if (menuTypeToDelete != null)
        {
            _orderingContext.MenuTypes.Remove(menuTypeToDelete);
            await _orderingContext.SaveChangesAsync();
        }
    }

    public async Task<List<MenuTypeReadDto>> GetAllMenuTypes()
    {
        var menuTypes = await _orderingContext.MenuTypes
            .Select(menu => new MenuTypeReadDto
            {
                Id = menu.Id,
                Name = menu.Name,
            })
            .ToListAsync();

        return menuTypes;
    }

    public async Task<MenuTypeReadDto> GetMenuType(Guid id)
    {
        var menuType = await _orderingContext.MenuTypes.FirstOrDefaultAsync(x => x.Id == id);

        return new MenuTypeReadDto
        {
            Id = id,
            Name = menuType.Name,
        };
    }

    public async Task<MenuTypeReadDto> UpdateMenuType(MenuTypeUpdateDto menuTypeUpdateDto, Guid id)
    {
        var menuTypeToUpdate = await _orderingContext.MenuTypes.FirstOrDefaultAsync(x => x.Id == id);

        menuTypeToUpdate.Name = menuTypeUpdateDto.Name;

        await _orderingContext.SaveChangesAsync();

        return new MenuTypeReadDto
        {
            Id = id,
            Name = menuTypeToUpdate.Name,
        };
    }
}
