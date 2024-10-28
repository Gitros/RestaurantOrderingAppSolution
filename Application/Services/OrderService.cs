using Application.Contracts;
using Application.Dtos.Orders;
using Domain;
using Infrastructure.Database;

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
        var order = new Order
        {
            Id = Guid.NewGuid(),
            OrderDateTime = orderCreateDto.OrderDateTime,
            TotalAmount = orderCreateDto.TotalAmount,
            OrderStatus = orderCreateDto.OrderStatus
        };

        var result = await _orderingContext.Orders.AddAsync(order);
        await _orderingContext.SaveChangesAsync();

        var createdOrder = new OrderReadDto
        {
            Id = result.Entity.Id,
            OrderDateTime = result.Entity.OrderDateTime,
            TotalAmount = result.Entity.TotalAmount,
            OrderStatus = result.Entity.OrderStatus
        };

        return createdOrder;
    }

    public Task DeleteOrder(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<OrderReadDto>> GetAllOrders()
    {
        throw new NotImplementedException();
    }

    public Task<OrderReadDto> GetOrder(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<OrderReadDto> UpdateOrder(OrderUpdateDto orderUpdateDto, Guid id)
    {
        throw new NotImplementedException();
    }
}
