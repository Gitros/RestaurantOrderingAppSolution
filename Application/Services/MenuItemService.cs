using Application.Contracts;
using Application.Dtos.Common;
using Application.Dtos.MenuItems;
using AutoMapper;
using Domain;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Services;

public class MenuItemService(RestaurantOrderingContext orderingContext, IMapper mapper) : IMenuItemService
{
    public async Task<ResultDto<MenuItemReadDto>> CreateMenuItem(MenuItemCreateDto menuItemCreateDto)
    {
        try
        {
            var menuItem = mapper.Map<MenuItem>(menuItemCreateDto);

            if (menuItemCreateDto.TagIds.Any())
            {
                var validTags = await orderingContext.Tags
                    .Where(t => menuItemCreateDto.TagIds.Contains(t.Id))
                    .ToListAsync();

                foreach (var tag in validTags)
                {
                    menuItem.MenuItemTags.Add(new MenuItemTag
                    {
                        MenuItemId = menuItem.Id,
                        TagId = tag.Id
                    });
                }
            }

            await orderingContext.MenuItems.AddAsync(menuItem);
            await orderingContext.SaveChangesAsync();

            var createdMenuItem = mapper.Map<MenuItemReadDto>(menuItem);

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
            var menuItems = await orderingContext.MenuItems
                .Include(mi => mi.MenuItemTags)
                .ThenInclude(mt => mt.Tag)
                .ToListAsync();

            var menuItemDtos = mapper.Map<List<MenuItemReadDto>>(menuItems);

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
            var menuItem = await orderingContext.MenuItems
                .Include(mi => mi.MenuCategory)
                .FirstOrDefaultAsync(mi => mi.Id == id);

            if (menuItem == null)
                return ResultDto<MenuItemReadDto>
                    .Failure("Menu item not found.", HttpStatusCode.NotFound);

            var menuItemDto = mapper.Map<MenuItemReadDto>(menuItem);

            return ResultDto<MenuItemReadDto>
                .Success(menuItemDto, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<MenuItemReadDto>
                .Failure($"An error occurred while fetching the menu item: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<List<MenuItemReadDto>>> GetMenuItemsByCategory(Guid categoryId)
    {
        try
        {
            var menuItems = await orderingContext.MenuItems
                .Where(mi => mi.MenuCategoryId == categoryId && !mi.IsDeleted)
                .Include(mi => mi.MenuItemTags)
                    .ThenInclude(mt => mt.Tag)
                .ToListAsync();

            var menuItemDtos = mapper.Map<List<MenuItemReadDto>>(menuItems);

            return ResultDto<List<MenuItemReadDto>>
                .Success(menuItemDtos, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<List<MenuItemReadDto>>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<MenuItemReadDto>> UpdateMenuItem(MenuItemUpdateDto menuItemUpdateDto, Guid id)
    {
        try
        {
            var menuItem = await orderingContext.MenuItems
                .Include(mi => mi.MenuItemTags)
                .FirstOrDefaultAsync(mi => mi.Id == id);

            if (menuItem == null)
                return ResultDto<MenuItemReadDto>
                    .Failure("Menu item not found.", HttpStatusCode.NotFound);

            mapper.Map(menuItemUpdateDto, menuItem);

            menuItem.MenuItemTags.Clear();

            if (menuItemUpdateDto.TagIds.Any())
            {
                var validTags = await orderingContext.Tags
                    .Where(t => menuItemUpdateDto.TagIds.Contains(t.Id))
                    .ToListAsync();

                foreach (var tag in validTags)
                {
                    menuItem.MenuItemTags.Add(new MenuItemTag
                    {
                        MenuItemId = menuItem.Id,
                        TagId = tag.Id
                    });
                }
            }

            await orderingContext.SaveChangesAsync();

            var updatedMenuItem = mapper.Map<MenuItemReadDto>(menuItem);

            return ResultDto<MenuItemReadDto>
                .Success(updatedMenuItem, HttpStatusCode.OK);
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
            var menuItemToDelete = await orderingContext.MenuItems.FindAsync(id);

            if (menuItemToDelete == null)
                return ResultDto<bool>
                    .Failure("Menu item not found.", HttpStatusCode.NotFound);

            menuItemToDelete.IsDeleted = true;
            menuItemToDelete.IsUsed = false;

            await orderingContext.SaveChangesAsync();

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
