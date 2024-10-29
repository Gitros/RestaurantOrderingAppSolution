using Application.Contracts;
using Application.Dtos.MenuItems;
using Domain;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class MenuItemService : IMenuItemService
{
    private readonly RestaurantOrderingContext _orderingContext;

    public MenuItemService(RestaurantOrderingContext orderingContext)
    {
        _orderingContext = orderingContext;
    }

    public async Task<MenuItemReadDto> CreateMenuItem(MenuItemCreateDto menuItemCreateDto)
    {
        var menuItem = new MenuItem
        {
            Id = Guid.NewGuid(),
            Name = menuItemCreateDto.Name,
            Description = menuItemCreateDto.Description,
            Price = menuItemCreateDto.Price
        };

        var result = await _orderingContext.MenuItems.AddAsync(menuItem);
        await _orderingContext.SaveChangesAsync();

        var createdMenuItem = new MenuItemReadDto
        {
            Id = result.Entity.Id,
            Name = result.Entity.Name,
            Description = result.Entity.Description,
            Price = result.Entity.Price
        };
        
        return createdMenuItem;
    }

    public async Task DeleteMenuItem(Guid id)
    {
        var menuItemToDelete = await _orderingContext.MenuItems.FindAsync(id);

        if(menuItemToDelete != null)
        {
            _orderingContext.MenuItems.Remove(menuItemToDelete);
            await _orderingContext.SaveChangesAsync();
        }
    }

    public async Task<List<MenuItemReadDto>> GetAllMenuItems()
    {
        var menuItems = await _orderingContext.MenuItems
            .Select(menuItems => new MenuItemReadDto
            {
                Id = Guid.NewGuid(),
                Name = menuItems.Name,
                Description = menuItems.Description,
                Price = menuItems.Price
            })
            .ToListAsync();

        return menuItems;
    }

    public async Task<MenuItemReadDto> GetMenuItem(Guid id)
    {
        var menuItem = await _orderingContext.MenuItems.FindAsync(id);

        return new MenuItemReadDto
        {
            Id = id,
            Name = menuItem.Name,
            Description = menuItem.Description,
            Price = menuItem.Price
        };
    }

    public async Task<MenuItemReadDto> UpdateMenuItem(MenuItemUpdateDto menuItemUpdateDto, Guid id)
    {
        var menuItemToUpdate = await _orderingContext.MenuItems.FindAsync(id);

        menuItemToUpdate.Name = menuItemUpdateDto.Name;
        menuItemToUpdate.Description = menuItemUpdateDto.Description;
        menuItemToUpdate.Price = menuItemUpdateDto.Price;

        await _orderingContext.SaveChangesAsync();

        return new MenuItemReadDto
        {
            Id = id,
            Name = menuItemToUpdate.Name,
            Description = menuItemToUpdate.Description,
            Price = menuItemToUpdate.Price
        };
    }
}
