using Application.Contracts;
using Application.Dtos.Common;
using Application.Dtos.OrderItems;
using AutoMapper;
using Domain;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Services;

public class OrderItemService : IOrderItemService
{
    private readonly RestaurantOrderingContext _orderingContext;
    private readonly IMapper _mapper;

    public OrderItemService(RestaurantOrderingContext orderingContext, IMapper mapper)
    {
        _orderingContext = orderingContext;
        _mapper = mapper;
    }

    public async Task<ResultDto<OrderItemReadDto>> CreateOrderItem(OrderItemCreateDto orderItemCreateDto, Guid orderId)
    {
        try
        {
            var order = await _orderingContext.Orders
                    .Include(o => o.OrderItems)
                    .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                return ResultDto<OrderItemReadDto>
                    .Failure("Order not found.", HttpStatusCode.NotFound);

            var orderItem = _mapper.Map<OrderItem>(orderItemCreateDto);
            orderItem.OrderId = orderId;

            await _orderingContext.OrderItems.AddAsync(orderItem);
            await _orderingContext.SaveChangesAsync();

            var createdOrderItem = _mapper.Map<OrderItemReadDto>(orderItem);

            return ResultDto<OrderItemReadDto>
                .Success(createdOrderItem, HttpStatusCode.Created);
        }
        catch (Exception ex)
        {
            return ResultDto<OrderItemReadDto>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<OrderItemReadDto>> GetOrderItem(Guid id)
    {
        try
        {
            var orderItem = await _orderingContext.OrderItems
                .Include(oi => oi.MenuItem)
                .FirstOrDefaultAsync(oi => oi.Id == id);

            if(orderItem == null)
                return ResultDto<OrderItemReadDto>
                    .Failure("Order item not found.", HttpStatusCode.NotFound);

            var orderItemDto = _mapper.Map<OrderItemReadDto>(orderItem);

            return ResultDto<OrderItemReadDto>
                .Success(orderItemDto, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<OrderItemReadDto>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<List<OrderItemReadDto>>> GetAllOrderItems()
    {
        try
        {
            var orderItems = await _orderingContext.OrderItems
                .Include(oi => oi.MenuItem)
                .Include(oi => oi.OrderItemIngredients)
                    .ThenInclude(oii => oii.Ingredient)
                .ToListAsync();

            var orderItemsDto = _mapper.Map<List<OrderItemReadDto>>(orderItems);

            return ResultDto<List<OrderItemReadDto>>
                .Success(orderItemsDto, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<List<OrderItemReadDto>>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<OrderItemReadDto>> UpdateOrderItem(OrderItemUpdateDto orderItemUpdateDto, Guid id)
    {
        try
        {
            var orderItemToUpdate = await _orderingContext.OrderItems
                .Include(oi => oi.MenuItem)
                .Include(oi => oi.OrderItemIngredients)
                .FirstOrDefaultAsync(oi => oi.Id == id);

            if (orderItemToUpdate == null)
                return ResultDto<OrderItemReadDto>
                    .Failure("Order item not found.", HttpStatusCode.NotFound);

            _mapper.Map(orderItemUpdateDto, orderItemToUpdate);

            orderItemToUpdate.OrderItemIngredients.Clear();

            if (orderItemUpdateDto.Ingredients.Any())
            {
                var validIngredients = await _orderingContext.Ingredients
                    .Where(i => orderItemUpdateDto.Ingredients.Select(oi => oi.IngredientId).Contains(i.Id) && !i.IsDeleted)
                    .ToListAsync();

                foreach (var ingredientDto in orderItemUpdateDto.Ingredients)
                {
                    var ingredient = validIngredients.FirstOrDefault(i => i.Id == ingredientDto.IngredientId);
                    if (ingredient != null)
                    {
                        orderItemToUpdate.OrderItemIngredients.Add(new OrderItemIngredient
                        {
                            IngredientId = ingredient.Id,
                            Quantity = ingredientDto.Quantity,
                            OrderItemId = orderItemToUpdate.Id
                        });
                    }
                }
            }

            await _orderingContext.SaveChangesAsync();

            var updatedOrderItem = _mapper.Map<OrderItemReadDto>(orderItemToUpdate);

            return ResultDto<OrderItemReadDto>
                .Success(updatedOrderItem, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<OrderItemReadDto>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<OrderItemReadDto>> UpdateOrderItemStatus(Guid orderId, Guid orderItemId, OrderItemStatusDto statusDto)
    {
        try
        {
            var orderItem = await _orderingContext.OrderItems
                .FirstOrDefaultAsync(oi => oi.Id == orderItemId && oi.OrderId == orderId);

            if (orderItem == null)
                return ResultDto<OrderItemReadDto>
                    .Failure("Order item not found.", HttpStatusCode.NotFound);

            if (!Enum.TryParse<OrderItemStatus>(statusDto.OrderItemStatus, true, out var newStatus))
            {
                return ResultDto<OrderItemReadDto>
                    .Failure("Invalid order item status provided.", HttpStatusCode.BadRequest);
            }

            orderItem.OrderItemStatus = newStatus;

            await _orderingContext.SaveChangesAsync();

            var updatedOrderItemDto = _mapper.Map<OrderItemReadDto>(orderItem);

            return ResultDto<OrderItemReadDto>
                .Success(updatedOrderItemDto, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<OrderItemReadDto>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<bool>> DeleteOrderItem(Guid id)
    {
        try
        {
            var orderItem = await _orderingContext.OrderItems.FirstOrDefaultAsync(oi => oi.Id == id);

            if (orderItem == null)
                return ResultDto<bool>
                    .Failure("Order Item not found.", HttpStatusCode.NotFound);

            _orderingContext.OrderItems.Remove(orderItem);
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