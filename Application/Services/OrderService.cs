using Application.Contracts;
using Application.Dtos.Common;
using Application.Dtos.OrderItems;
using Application.Dtos.Orders;
using AutoMapper;
using Domain;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Application.Services;

public class OrderService : IOrderService
{
    private readonly RestaurantOrderingContext _orderingContext;
    private readonly IMapper _mapper;

    public OrderService(RestaurantOrderingContext orderingContext, IMapper mapper)
    {
        _orderingContext = orderingContext;
        _mapper = mapper;
    }

    public async Task<ResultDto<OrderReadDto>> CreateOrder(OrderCreateDto orderCreateDto)
    {
        try
        {
            
            //Validator.ValidateObject(orderCreateDto, new ValidationContext(orderCreateDto));
            if (!ValidateOrderItems())
            {
                return ResultDto<OrderReadDto>
                    .Failure("Order items invalid", HttpStatusCode.BadRequest);
            }

            var order = _mapper.Map<Order>(orderCreateDto);


            foreach(var item in order.OrderItems)
            {
                item.OrderId = order.Id;
            }

            await _orderingContext.Orders.AddAsync(order);
            await _orderingContext.SaveChangesAsync();

            var createdOrder = _mapper.Map<OrderReadDto>(order);

            return ResultDto<OrderReadDto>
                .Success(createdOrder, HttpStatusCode.Created);
        } 
        catch (Exception ex)
        {
            return ResultDto<OrderReadDto>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    private bool ValidateOrderItems()
    {
        return true;
    }

    public async Task<ResultDto<OrderReadDto>> GetOrder(Guid id)
    {
        var order = await _orderingContext.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null)
        {
            return ResultDto<OrderReadDto>.Failure("Order not found", HttpStatusCode.NotFound);
        }

        var orderDto = _mapper.Map<OrderReadDto>(order);
        return ResultDto<OrderReadDto>.Success(orderDto, HttpStatusCode.OK);
    }

    public async Task<ResultDto<List<OrderReadDto>>> GetAllOrders()
    {
        var orders = await _orderingContext.Orders
            .Include(o => o.OrderItems)
            .ToListAsync();

        var ordersDto = _mapper.Map<List<OrderReadDto>>(orders);
        return ResultDto<List<OrderReadDto>>.Success(ordersDto, HttpStatusCode.OK);
    }

    public async Task<ResultDto<OrderReadDto>> UpdateOrder(OrderUpdateDto orderUpdateDto, Guid id)
    {
        var orderToUpdate = await _orderingContext.Orders.FindAsync(id);

        if (orderToUpdate == null)
        {
            return ResultDto<OrderReadDto>.Failure("Order not found", HttpStatusCode.NotFound);
        }

        _mapper.Map(orderUpdateDto, orderToUpdate);
        await _orderingContext.SaveChangesAsync();

        var updatedOrderDto = _mapper.Map<OrderReadDto>(orderToUpdate);
        return ResultDto<OrderReadDto>.Success(updatedOrderDto, HttpStatusCode.OK);
    }

    public async Task<ResultDto<OrderReadDto>> UpdateOrderStatus(OrderStatus newStatus, Guid id)
    {
        var order = await _orderingContext.Orders.FindAsync(id);

        if (order == null)
        {
            return ResultDto<OrderReadDto>.Failure("Order not found", HttpStatusCode.NotFound);
        }

        order.OrderStatus = newStatus;
        await _orderingContext.SaveChangesAsync();

        var updatedOrderDto = _mapper.Map<OrderReadDto>(order);
        return ResultDto<OrderReadDto>.Success(updatedOrderDto, HttpStatusCode.OK);
    }

    public async Task<ResultDto<bool>> DeleteOrder(Guid id)
    {
        var orderToDelete = await _orderingContext.Orders.FindAsync(id);

        if(orderToDelete == null)
        {
            return ResultDto<bool>.Failure("ordernotfound", HttpStatusCode.NotFound);
        }

        _orderingContext.Orders.Remove(orderToDelete);
        await _orderingContext.SaveChangesAsync();

        return ResultDto<bool>.Success(true, HttpStatusCode.NoContent);
    }
}