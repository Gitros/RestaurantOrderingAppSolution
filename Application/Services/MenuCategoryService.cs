using Application.Contracts;
using Application.Dtos.Common;
using Application.Dtos.MenuCategories;
using AutoMapper;
using Domain;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Services;

public class MenuCategoryService : IMenuCategoryService
{
    private RestaurantOrderingContext _orderingContext;
    private readonly IMapper _mapper;

    public MenuCategoryService(RestaurantOrderingContext orderingContext, IMapper mapper)
    {
        _orderingContext = orderingContext;
        _mapper = mapper;
    }

    public async Task<ResultDto<MenuCategoryReadDto>> CreateMenuCategory(MenuCategoryCreateDto menuCategoryCreateDto)
    {
        try
        {
            var menuCategory = _mapper.Map<MenuCategory>(menuCategoryCreateDto);

            await _orderingContext.MenuCategories.AddAsync(menuCategory);
            await _orderingContext.SaveChangesAsync();

            var createdMenuCategory = _mapper.Map<MenuCategoryReadDto>(menuCategory);

            return ResultDto<MenuCategoryReadDto>
                .Success(createdMenuCategory, HttpStatusCode.Created);
        }
        catch (Exception ex)
        {
            return ResultDto<MenuCategoryReadDto>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<List<MenuCategoryReadDto>>> GetAllMenuCategories()
    {
        try
        {
            var menuCategories = await _orderingContext.MenuCategories
                .Include(mc => mc.MenuItems)
                .ToListAsync();

            var menuCategoryDtos = _mapper.Map<List<MenuCategoryReadDto>>(menuCategories);

            return ResultDto<List<MenuCategoryReadDto>>
                .Success(menuCategoryDtos, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<List<MenuCategoryReadDto>>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<MenuCategoryReadDto>> GetMenuCategory(Guid id)
    {
        try
        {
            var menuCategory = await _orderingContext.MenuCategories
                .Include(mc => mc.MenuItems)
                .FirstOrDefaultAsync(mc => mc.Id == id);

            if (menuCategory == null)
                return ResultDto<MenuCategoryReadDto>
                    .Failure("MenuCategory not found.", HttpStatusCode.NotFound);

            var menuCategoryDto = _mapper.Map<MenuCategoryReadDto>(menuCategory);

            return ResultDto<MenuCategoryReadDto>
                .Success(menuCategoryDto, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<MenuCategoryReadDto>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<MenuCategoryReadDto>> UpdateMenuCategory(MenuCategoryUpdateDto menuCategoryUpdateDto, Guid id)
    {
        try
        {
            var menuCategoryToUpdate = await _orderingContext.MenuCategories.FindAsync(id);

            if (menuCategoryToUpdate == null)
                return ResultDto<MenuCategoryReadDto>
                    .Failure("MenuCategory not found or has been deleted.", HttpStatusCode.NotFound);

            _mapper.Map(menuCategoryUpdateDto, menuCategoryToUpdate);
            await _orderingContext.SaveChangesAsync();

            var updatedMenuCategory = _mapper.Map<MenuCategoryReadDto>(menuCategoryToUpdate);

            return ResultDto<MenuCategoryReadDto>
                .Success(updatedMenuCategory, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<MenuCategoryReadDto>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<bool>> DeleteMenuCategory(Guid id)
    {
        try
        {
            var menuCategory = await _orderingContext.MenuCategories.FindAsync(id);
            if (menuCategory == null)
                return ResultDto<bool>
                    .Failure("MenuCategory not found.", HttpStatusCode.NotFound);

            menuCategory.IsDeleted = true;
            menuCategory.IsUsed = false;

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
