﻿using Application.Contracts;
using Application.Dtos.Common;
using Application.Dtos.OrderItemIngredients;
using Application.Dtos.OrderItems;
using Application.Dtos.Orders;
using AutoMapper;
using Domain;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Services;

public class OrderItemService(RestaurantOrderingContext orderingContext, IMapper mapper) : IOrderItemService
{
    public async Task<ResultDto<OrderReadDto>> AddOrderItem(OrderItemCreateDto orderItemDto, Guid orderId)
    {
        try
        {
            var order = await orderingContext.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                return ResultDto<OrderReadDto>
                    .Failure("Order not found.", HttpStatusCode.NotFound);

            var menuItem = await orderingContext.MenuItems
                .FirstOrDefaultAsync(mi => mi.Id == orderItemDto.MenuItemId);

            if (menuItem == null)
                return ResultDto<OrderReadDto>
                    .Failure($"MenuItem with ID {orderItemDto.MenuItemId} not found.", HttpStatusCode.BadRequest);

            var existingItem = order.OrderItems.FirstOrDefault(oi => oi.MenuItemId == orderItemDto.MenuItemId);
            if (existingItem != null)
            {
                existingItem.Quantity += orderItemDto.Quantity;
                order.TotalAmount += menuItem.Price * orderItemDto.Quantity;
            }
            else
            {
                var orderItem = mapper.Map<OrderItem>(orderItemDto);
                orderItem.Price = menuItem.Price;
                orderItem.OrderId = orderId;

                await orderingContext.OrderItems.AddAsync(orderItem);
                order.TotalAmount += orderItem.Price * orderItem.Quantity;
            }

            await orderingContext.SaveChangesAsync();

            var updatedOrderDto = mapper.Map<OrderReadDto>(order);
            return ResultDto<OrderReadDto>
                .Success(updatedOrderDto, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<OrderReadDto>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<OrderReadDto>> AddOrderItems(IEnumerable<OrderItemCreateDto> orderItemDtos, Guid orderId)
    {
        try
        {
            var order = await orderingContext.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                return ResultDto<OrderReadDto>
                    .Failure("Order not found.", HttpStatusCode.NotFound);

            var menuItemIds = orderItemDtos
                .Select(dto => dto.MenuItemId)
                .Distinct();

            var menuItems = await orderingContext.MenuItems
                .Where(mi => menuItemIds.Contains(mi.Id))
                .ToDictionaryAsync(mi => mi.Id);

            foreach (var dto in orderItemDtos)
            {
                if (!menuItems.TryGetValue(dto.MenuItemId, out var menuItem))
                    return ResultDto<OrderReadDto>
                        .Failure($"MenuItem with ID {dto.MenuItemId} not found.", HttpStatusCode.BadRequest);

                var existingItem = order.OrderItems
                    .FirstOrDefault(oi => oi.MenuItemId == dto.MenuItemId);
                if (existingItem != null)
                {
                    existingItem.Quantity += dto.Quantity;
                    order.TotalAmount += menuItem.Price * dto.Quantity;
                }
                else
                {
                    var orderItem = mapper.Map<OrderItem>(dto);
                    orderItem.Price = menuItem.Price;
                    orderItem.OrderId = orderId;

                    await orderingContext.OrderItems.AddAsync(orderItem);
                    order.TotalAmount += orderItem.Price * orderItem.Quantity;
                }
            }

            await orderingContext.SaveChangesAsync();

            var updatedOrderDto = mapper.Map<OrderReadDto>(order);
            return ResultDto<OrderReadDto>
                .Success(updatedOrderDto, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<OrderReadDto>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }


    public async Task<ResultDto<OrderItemReadDto>> GetOrderItem(Guid id)
    {
        try
        {
            var orderItem = await orderingContext.OrderItems
                .Include(oi => oi.MenuItem)
                .FirstOrDefaultAsync(oi => oi.Id == id);

            if (orderItem == null)
                return ResultDto<OrderItemReadDto>
                    .Failure("Order item not found.", HttpStatusCode.NotFound);

            var orderItemDto = mapper.Map<OrderItemReadDto>(orderItem);

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
            var orderItems = await orderingContext.OrderItems
                .Include(oi => oi.MenuItem)
                .Include(oi => oi.OrderItemIngredients)
                    .ThenInclude(oii => oii.Ingredient)
                .ToListAsync();

            var orderItemsDto = mapper.Map<List<OrderItemReadDto>>(orderItems);

            return ResultDto<List<OrderItemReadDto>>
                .Success(orderItemsDto, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<List<OrderItemReadDto>>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<OrderReadDto>> ApplyOrderItemDiscount(decimal discountPercentage, Guid orderId, Guid orderItemId)
    {
        try
        {
            var order = await orderingContext.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                return ResultDto<OrderReadDto>.Failure("Order not found.", HttpStatusCode.NotFound);

            var item = order.OrderItems.FirstOrDefault(oi => oi.Id == orderItemId);
            if (item == null)
                return ResultDto<OrderReadDto>.Failure("Order item not found.", HttpStatusCode.NotFound);

            if (discountPercentage < 0 || discountPercentage > 100)
                return ResultDto<OrderReadDto>.Failure("Invalid discount percentage.", HttpStatusCode.BadRequest);

            item.Discount = discountPercentage;

            item.Price = item.Price * (1 - discountPercentage / 100);

            order.TotalAmount = RecalculateOrderTotal(order);

            await orderingContext.SaveChangesAsync();

            var updatedOrder = mapper.Map<OrderReadDto>(order);
            return ResultDto<OrderReadDto>.Success(updatedOrder, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<OrderReadDto>.Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<OrderItemReadDto>> UpdateOrderItem(OrderItemUpdateDto updateDto, Guid orderItemId, Guid orderId)
    {
        try
        {
            var orderItem = await orderingContext.OrderItems
                .Include(oi => oi.OrderItemIngredients)
                .FirstOrDefaultAsync(oi => oi.Id == orderItemId && oi.OrderId == orderId);

            if (orderItem == null)
                return ResultDto<OrderItemReadDto>
                    .Failure("Order item not found.", HttpStatusCode.NotFound);

            if(updateDto.Quantity > 0) 
                orderItem.Quantity = updateDto.Quantity;

            if(!string.IsNullOrWhiteSpace(updateDto.SpecialInstructions))
                orderItem.SpecialInstructions = updateDto.SpecialInstructions;

            if (updateDto.Ingredients.Any())
            {
                var validIngredients = await orderingContext.Ingredients
                    .Where(i => updateDto.Ingredients
                        .Select(oi => oi.IngredientId)
                        .Contains(i.Id) && !i.IsDeleted)
                    .ToListAsync();

                foreach (var ingredientDto in updateDto.Ingredients)
                {
                    var existingIngredient = orderItem.OrderItemIngredients
                        .FirstOrDefault(oii => oii.IngredientId == ingredientDto.IngredientId);

                    if(existingIngredient != null)
                    {
                        existingIngredient.Quantity = ingredientDto.Quantity;
                    }
                    else
                    {
                        var ingredient = validIngredients.FirstOrDefault(i => i.Id == ingredientDto.IngredientId);
                        if(ingredient != null)
                        {
                            orderItem.OrderItemIngredients.Add(new OrderItemIngredient
                            {
                                IngredientId = ingredient.Id,
                                Quantity = ingredientDto.Quantity,
                                OrderItemId = orderItem.Id
                            });
                        }
                    }
                }

                var ingredientIdsToKeep = updateDto.Ingredients
                    .Select(dto => dto.IngredientId)
                    .ToList();

                orderItem.OrderItemIngredients.RemoveAll(oii => !ingredientIdsToKeep.Contains(oii.IngredientId));
            }

            await orderingContext.SaveChangesAsync();

            var updatedOrderItem = mapper.Map<OrderItemReadDto>(orderItem);

            return ResultDto<OrderItemReadDto>
                .Success(updatedOrderItem, HttpStatusCode.OK);
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
            var orderItem = await orderingContext.OrderItems.FirstOrDefaultAsync(oi => oi.Id == id);

            if (orderItem == null)
                return ResultDto<bool>
                    .Failure("Order Item not found.", HttpStatusCode.NotFound);

            orderingContext.OrderItems.Remove(orderItem);
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

    private decimal RecalculateOrderTotal(Order order)
    {
        var total = order.OrderItems.Sum(oi => oi.Price * oi.Quantity);

        if (order.Discount > 0)
        {
            total = total * (1 - order.Discount / 100);
        }

        return total;
    }
}