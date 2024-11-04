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
    private RestaurantOrderingContext _orderingContext;
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

        return new OrderReadDto
        {
            Id = order.Id,
            OrderDateTime = order.OrderDateTime,
            TotalAmount = order.TotalAmount,
            OrderStatus = order.OrderStatus,
            TableId = order.TableId,
            OrderItems = order.OrderItems.Select(oi => new OrderItemReadDto
            {
                Id = oi.Id,
                Price = oi.Price,
                Quantity = oi.Quantity,
                SpecialInstructions = oi.SpecialInstructions,
                OrderId = oi.OrderId,
                MenuItemId = oi.MenuItemId
            }).ToList()
        };
    }

    public async Task<List<OrderReadDto>> GetAllOrders()
    {
        var orders = await _orderingContext.Orders
            .Select(orders => new OrderReadDto
            {
                Id = orders.Id,
                OrderDateTime = orders.OrderDateTime,
                TotalAmount = orders.TotalAmount,
                OrderStatus = orders.OrderStatus,
                TableId = orders.TableId,
                OrderItems = orders.OrderItems.Select(oi => new OrderItemReadDto
                {
                    Id = oi.Id,
                    Price = oi.Price,
                    Quantity = oi.Quantity,
                    SpecialInstructions = oi.SpecialInstructions,
                    OrderId = oi.OrderId,
                    MenuItemId = oi.MenuItemId
                }).ToList()
            }).ToListAsync();

        return orders;
    }

    public async Task<OrderReadDto> UpdateOrder(OrderUpdateDto orderUpdateDto, Guid id)
    {
        var orderToUpdate = await _orderingContext.Orders.FindAsync(id);

        orderToUpdate.TotalAmount = orderUpdateDto.TotalAmount;
        orderToUpdate.OrderStatus = orderUpdateDto.OrderStatus;
        orderToUpdate.TableId = orderUpdateDto.TableId;

        await _orderingContext.SaveChangesAsync();

        return new OrderReadDto
        {
            Id = id,
            TotalAmount = orderUpdateDto.TotalAmount,
            OrderStatus = orderUpdateDto.OrderStatus,
            TableId = orderToUpdate.TableId
        };
    }

    public async Task<OrderReadDto> UpdateOrderStatus(OrderStatus newStatus, Guid id)
    {
        var orderStatus = await _orderingContext.Orders.FindAsync(id);

        orderStatus.OrderStatus = newStatus;

        await _orderingContext.SaveChangesAsync();

        return new OrderReadDto
        {
            Id = id,
            TotalAmount = orderStatus.TotalAmount,
            OrderStatus = orderStatus.OrderStatus,
            TableId = orderStatus.TableId
        };
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
