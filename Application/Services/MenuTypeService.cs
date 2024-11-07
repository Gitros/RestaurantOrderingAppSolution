using Application.Contracts;
using Application.Dtos.Common;
using Application.Dtos.MenuTypes;
using Application.Dtos.Orders;
using AutoMapper;
using Domain;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Net;

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

    public async Task<ResultDto<MenuTypeReadDto>> CreateMenuType(MenuTypeCreateDto menuTypeCreateDto)
    {
        try
        {
            var menuType = _mapper.Map<MenuType>(menuTypeCreateDto);

            await _orderingContext.MenuTypes.AddAsync(menuType);
            await _orderingContext.SaveChangesAsync();

            var createdMenuType = _mapper.Map<MenuTypeReadDto>(menuType);

            return ResultDto<MenuTypeReadDto>
                .Success(createdMenuType, HttpStatusCode.Created);
        }
        catch (Exception ex)
        {
            return ResultDto<MenuTypeReadDto>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<List<MenuTypeReadDto>>> GetAllMenuTypes()
    {
        try
        {
            var menuTypes = await _orderingContext.MenuTypes
                .Include(o => o.MenuItems)
                .ToListAsync();

            var menuTypeDtos = _mapper.Map<List<MenuTypeReadDto>>(menuTypes);

            return ResultDto<List<MenuTypeReadDto>>
                .Success(menuTypeDtos, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<List<MenuTypeReadDto>>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<MenuTypeReadDto>> GetMenuType(Guid id)
    {
        try
        {
            var menuType = await _orderingContext.MenuTypes
                .Include(o => o.MenuItems)
                .FirstOrDefaultAsync(x => x.Id == id);

            var menuTypeDto = _mapper.Map<MenuTypeReadDto>(menuType);

            return ResultDto<MenuTypeReadDto>.Success(menuTypeDto, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<MenuTypeReadDto>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<MenuTypeReadDto>> UpdateMenuType(MenuTypeUpdateDto menuTypeUpdateDto, Guid id)
    {
        try
        {
            var menuTypeToUpdate = await _orderingContext.MenuTypes.FindAsync(id);

            _mapper.Map(menuTypeToUpdate, menuTypeUpdateDto);
            await _orderingContext.SaveChangesAsync();

            var updatedMenuType = _mapper.Map<MenuTypeReadDto>(menuTypeToUpdate);

            return ResultDto<MenuTypeReadDto>.Success(updatedMenuType, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<MenuTypeReadDto>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<bool>> DeleteMenuType(Guid id)
    {
        try
        {
            var menuType = await _orderingContext.MenuTypes.FindAsync(id);
            if (menuType == null)
            {
                return ResultDto<bool>.Failure("MenuType not found.", HttpStatusCode.NotFound);
            }

            menuType.IsDeleted = true;
            _orderingContext.MenuTypes.Update(menuType);
            await _orderingContext.SaveChangesAsync();

            return ResultDto<bool>.Success(true, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<bool>.Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }
}
