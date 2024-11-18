using Application.Contracts;
using Application.Dtos.Common;
using Application.Dtos.MenuItems;
using AutoMapper;
using Domain;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Services;

public class MenuItemService : IMenuItemService
{
    private readonly RestaurantOrderingContext _orderingContext;
    private readonly IMapper _mapper;

    public MenuItemService(RestaurantOrderingContext orderingContext, IMapper mapper)
    {
        _orderingContext = orderingContext;
        _mapper = mapper;
    }

    public async Task<ResultDto<MenuItemReadDto>> CreateMenuItem(MenuItemCreateDto menuItemCreateDto)
    {
        try
        {
            var menuItem = _mapper.Map<MenuItem>(menuItemCreateDto);

            await _orderingContext.MenuItems.AddAsync(menuItem);
            await _orderingContext.SaveChangesAsync();

            var createdMenuItem = _mapper.Map<MenuItemReadDto>(menuItem);

            return ResultDto<MenuItemReadDto>
                .Success(createdMenuItem, HttpStatusCode.Created);

        }
        catch (Exception ex)
        {
            return ResultDto<MenuItemReadDto>
                   .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<List<MenuItemReadDto>>> GetAllMenuItems()
    {
        try
        {
            var menuItems = await _orderingContext.MenuItems
                .Include(mi => mi.MenuType)
                .ToListAsync();

            var menuItemDtos = _mapper.Map<List<MenuItemReadDto>>(menuItems);

            return ResultDto<List<MenuItemReadDto>>
                .Success(menuItemDtos, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<List<MenuItemReadDto>>
                   .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<MenuItemReadDto>> GetMenuItem(Guid id)
    {
        try
        {
            var menuItem = await _orderingContext.MenuItems
                .Include(mi => mi.MenuType)
                .FirstOrDefaultAsync(mi => mi.Id == id);

            if(menuItem == null) 
                return ResultDto<MenuItemReadDto>
                    .Failure("Menu item not found.", HttpStatusCode.NotFound);

            var menuItemDto = _mapper.Map<MenuItemReadDto>(menuItem);

            return ResultDto<MenuItemReadDto>
                .Success(menuItemDto, HttpStatusCode.OK);

        }
        catch (Exception ex)
        {
            return ResultDto<MenuItemReadDto>
                   .Failure($"An error occurred while fetching the menu item: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<MenuItemReadDto>> UpdateMenuItem(MenuItemUpdateDto menuItemUpdateDto, Guid id)
    {
        try
        {
            var menuItemToUpdate = await _orderingContext.MenuItems
                .Include(mi => mi.MenuType) 
                .FirstOrDefaultAsync(mi => mi.Id == id);

            if (menuItemToUpdate == null)
                return ResultDto<MenuItemReadDto>
                    .Failure("Menu item not found.", HttpStatusCode.NotFound);

            _mapper.Map(menuItemUpdateDto, menuItemToUpdate);

            await _orderingContext.SaveChangesAsync();

            var updatedMenuItemDto = _mapper.Map<MenuItemReadDto>(menuItemToUpdate);

            return ResultDto<MenuItemReadDto>
                .Success(updatedMenuItemDto, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<MenuItemReadDto>
                   .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<bool>> DeleteMenuItem(Guid id)
    {
        try
        {
            var menuItemToDelete = await _orderingContext.MenuItems.FindAsync(id);

            if (menuItemToDelete == null)
                return ResultDto<bool>
                    .Failure("Menu item not found.", HttpStatusCode.NotFound);

            menuItemToDelete.IsDeleted = true;
            menuItemToDelete.IsUsed = false;

            await _orderingContext.SaveChangesAsync();

            return ResultDto<bool>
                .Success(true, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<bool>
                   .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }
}
