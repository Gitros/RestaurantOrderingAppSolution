using Application.Contracts;
using Application.Dtos.Common;
using Application.Dtos.Orders;
using AutoMapper;
using Domain;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Services;

public class OrderService(RestaurantOrderingContext orderingContext, IMapper mapper) : IOrderService
{
    public async Task<ResultDto<OrderReadDto>> CreateOrder(OrderCreateDto orderCreateDto)
    {
        try
        {
            var order = mapper.Map<Order>(orderCreateDto);

            if(orderCreateDto.OrderType == OrderType.DineIn)
            {
                if(!orderCreateDto.TableId.HasValue)
                {
                    return ResultDto<OrderReadDto>
                        .Failure("TableId is required for DineIn orders.", HttpStatusCode.BadRequest);
                }
                order.TableId = orderCreateDto.TableId.Value;
            }

            await orderingContext.Orders.AddAsync(order);
            await orderingContext.SaveChangesAsync();

            var createdOrder = mapper.Map<OrderReadDto>(order);

            return ResultDto<OrderReadDto>
                .Success(createdOrder, HttpStatusCode.Created);
        } 
        catch (Exception ex)
        {
            return ResultDto<OrderReadDto>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<OrderReadDto>> GetOrder(Guid id)
    {
        try
        {
            var order = await orderingContext.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.OrderItemIngredients)
                    .ThenInclude(oii => oii.Ingredient)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return ResultDto<OrderReadDto>
                    .Failure("Order not found", HttpStatusCode.NotFound);

            var orderDto = mapper.Map<OrderReadDto>(order);

            return ResultDto<OrderReadDto>
                .Success(orderDto, HttpStatusCode.OK);

        }
        catch (Exception ex)
        {
            return ResultDto<OrderReadDto>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<List<OrderReadDto>>> GetAllOrders()
    {
        try
        {
            var orders = await orderingContext.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                .ToListAsync();

            var ordersDto = mapper.Map<List<OrderReadDto>>(orders);

            return ResultDto<List<OrderReadDto>>
                .Success(ordersDto, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<List<OrderReadDto>>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<OrderReadDto>> UpdateOrder(OrderUpdateDto orderUpdateDto, Guid id)
    {
        try
        {
            var orderToUpdate = await orderingContext.Orders
                    .Include(o => o.OrderItems)
                    .FirstOrDefaultAsync(o => o.Id == id);


            if (orderToUpdate == null)
                return ResultDto<OrderReadDto>
                    .Failure("Order not found", HttpStatusCode.NotFound);

            mapper.Map(orderUpdateDto, orderToUpdate);

            await orderingContext.SaveChangesAsync();

            var updatedOrderDto = mapper.Map<OrderReadDto>(orderToUpdate);

            return ResultDto<OrderReadDto>
                .Success(updatedOrderDto, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return ResultDto<OrderReadDto>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<OrderReadDto>> UpdateOrderStatus(OrderStatus newStatus, Guid id)
    {
        try
        {
            var order = await orderingContext.Orders
                    .Include(o => o.OrderItems)
                    .FirstOrDefaultAsync(o => o.Id == id);


            if (order == null)
                return ResultDto<OrderReadDto>
                    .Failure("Order not found", HttpStatusCode.NotFound);

            order.OrderStatus = newStatus;
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

    public async Task<ResultDto<bool>> DeleteOrder(Guid id)
    {
        try
        {
            var orderToDelete = await orderingContext.Orders
                    .Include(o => o.OrderItems)
                    .FirstOrDefaultAsync(o => o.Id == id);


            if (orderToDelete == null)
            {
                return ResultDto<bool>
                    .Failure("order not found", HttpStatusCode.NotFound);
            }

            orderingContext.Orders.Remove(orderToDelete);
            await orderingContext.SaveChangesAsync();

            return ResultDto<bool>
                .Success(true, HttpStatusCode.NoContent);
        }
        catch (Exception ex)
        {
            return ResultDto<bool>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }
}