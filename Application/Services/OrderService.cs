using Application.Contracts;
using Application.Dtos.Common;
using Application.Dtos.OrderItems;
using Application.Dtos.Orders;
using AutoMapper;
using Domain;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
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

    public async Task<CreateResultDto<OrderReadDto>> CreateOrder(OrderCreateDto orderCreateDto)
    {
        try
        {
            if(!ValidateOrderItems())
            {
                return new CreateResultDto<OrderReadDto>
                {
                    ErrorMessage = "Order items invalid",
                    HttpStatusCode = HttpStatusCode.BadRequest,
                };
            }

            var order = _mapper.Map<Order>(orderCreateDto);

            foreach(var item in order.OrderItems)
            {
                item.OrderId = order.Id;
            }

            await _orderingContext.Orders.AddAsync(order);
            await _orderingContext.SaveChangesAsync();

            var createdOrder = _mapper.Map<OrderReadDto>(order);

            return new CreateResultDto<OrderReadDto>
            {
                EntityResult = createdOrder,
                HttpStatusCode = HttpStatusCode.Created
            };
        } 
        catch (Exception ex)
        {
            return new CreateResultDto<OrderReadDto>
            {
                IsSuccess = false,
                ErrorMessage = ex.Message,
                HttpStatusCode = HttpStatusCode.InternalServerError
            };
        }
    }

    private bool ValidateOrderItems()
    {
        return true;
    }

    public async Task<OrderReadDto> GetOrder(Guid id)
    {
        var order = await _orderingContext.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null) return null;

        return _mapper.Map<OrderReadDto>(order);
    }

    public async Task<List<OrderReadDto>> GetAllOrders()
    {
        var orders = await _orderingContext.Orders
            .Include(o => o.OrderItems)
            .ToListAsync();

        return _mapper.Map<List<OrderReadDto>>(orders);
    }

    public async Task<OrderReadDto> UpdateOrder(OrderUpdateDto orderUpdateDto, Guid id)
    {
        var orderToUpdate = await _orderingContext.Orders.FindAsync(id);

        _mapper.Map(orderUpdateDto, orderToUpdate);

        await _orderingContext.SaveChangesAsync();

        return _mapper.Map<OrderReadDto>(orderToUpdate);
    }

    public async Task<OrderReadDto> UpdateOrderStatus(OrderStatus newStatus, Guid id)
    {
        var order = await _orderingContext.Orders.FindAsync(id);

        order.OrderStatus = newStatus;

        await _orderingContext.SaveChangesAsync();

        return _mapper.Map<OrderReadDto>(order);
    }
    public async Task DeleteOrder(Guid id)
    {
        var orderToDelete = await _orderingContext.Orders.FindAsync(id);

        if(orderToDelete != null)
        {
            _orderingContext.Orders.Remove(orderToDelete);
            await _orderingContext.SaveChangesAsync();
        }
    }

}
