using Application.Contracts;
using Application.Dtos.OrderItems;
using Application.Dtos.Orders;
using Domain;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class OrderService : IOrderService
{
    private RestaurantOrderingContext _orderingContext;

    public OrderService(RestaurantOrderingContext orderingContext)
    {
        _orderingContext = orderingContext;
    }

    public async Task<OrderReadDto> CreateOrder(OrderCreateDto orderCreateDto)
    {
        var orderId = Guid.NewGuid();
        var order = new Order
        {
            Id = orderId,
            OrderDateTime = orderCreateDto.OrderDateTime,
            TotalAmount = orderCreateDto.TotalAmount,
            OrderStatus = orderCreateDto.OrderStatus,
            TableId = orderCreateDto.TableId,
            OrderItems = orderCreateDto.OrderItems.Select(oi => new OrderItem
            {
                Id = oi.Id,
                Price = oi.Price,
                Quantity = oi.Quantity,
                OrderId = orderId,
                SpecialInstructions = oi.SpecialInstructions,
                MenuItemId = oi.MenuItemId
            }).ToList()
        };

        var result = await _orderingContext.Orders.AddAsync(order);
        await _orderingContext.SaveChangesAsync();

        var createdOrder = new OrderReadDto
        {
            Id = result.Entity.Id,
            OrderDateTime = result.Entity.OrderDateTime,
            TotalAmount = result.Entity.TotalAmount,
            OrderStatus = result.Entity.OrderStatus,
            TableId = result.Entity.TableId,
            OrderItems = result.Entity.OrderItems.Select(oi => new OrderItemReadDto
            {
                Id = oi.Id,
                Price = oi.Price,
                Quantity = oi.Quantity,
                SpecialInstructions = oi.SpecialInstructions,
                OrderId= oi.OrderId,
                MenuItemId = oi.MenuItemId
            }).ToList()
        };

        return createdOrder;
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
