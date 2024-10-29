using Application.Contracts;
using Application.Dtos.OrderItems;
using Domain;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class OrderItemService : IOrderItemService
{
    private readonly RestaurantOrderingContext _orderingContext;

    public OrderItemService(RestaurantOrderingContext orderingContext)
    {
        _orderingContext = orderingContext;
    }

    public async Task<OrderItemReadDto> CreateOrderItem(OrderItemCreateDto orderItemCreateDto)
    {
        var orderItem = new OrderItem
        {
            Id = Guid.NewGuid(),
            Price = orderItemCreateDto.Price,
            Quantity = orderItemCreateDto.Quantity,
            SpecialInstructions = orderItemCreateDto.SpecialInstructions,
            OrderId = orderItemCreateDto.OrderId,
            MenuItemId = orderItemCreateDto.MenuItemId
        };

        var result = await _orderingContext.OrderItems.AddAsync(orderItem);
        await _orderingContext.SaveChangesAsync();

        var createdOrderItem = new OrderItemReadDto
        {
            Id = result.Entity.Id,
            Price = result.Entity.Price,
            Quantity = result.Entity.Quantity,
            SpecialInstructions = result.Entity.SpecialInstructions,
            OrderId = result.Entity.OrderId,
            MenuItemId = result.Entity.MenuItemId
        };

        return createdOrderItem;
    }

    public async Task<OrderItemReadDto> GetOrderItem(Guid id)
    {
        var orderItem = await _orderingContext.OrderItems.FindAsync(id);

        return new OrderItemReadDto
        {
            Id = id,
            Price = orderItem.Price,
            Quantity = orderItem.Quantity,
            SpecialInstructions = orderItem.SpecialInstructions,
            OrderId = orderItem.OrderId,
            MenuItemId = orderItem.MenuItemId
        };
    }

    public async Task<List<OrderItemReadDto>> GetAllOrderItems()
    {
        var orderItems = await _orderingContext.OrderItems
            .Select(orderItems => new OrderItemReadDto
            {
                Id = orderItems.Id,
                Price = orderItems.Price,
                Quantity = orderItems.Quantity,
                SpecialInstructions = orderItems.SpecialInstructions,
                OrderId = orderItems.OrderId,
                MenuItemId = orderItems.MenuItemId
            })
            .ToListAsync();

        return orderItems;
    }

    public async Task<OrderItemReadDto> UpdateOrderItem(OrderItemUpdateDto orderItemUpdateDto, Guid id)
    {
        var orderItemToUpdate = await _orderingContext.OrderItems.FindAsync(id);

        orderItemToUpdate.Price = orderItemUpdateDto.Price;
        orderItemToUpdate.Quantity = orderItemUpdateDto.Quantity;
        orderItemToUpdate.SpecialInstructions = orderItemUpdateDto.SpecialInstructions;
        orderItemToUpdate.MenuItemId = orderItemUpdateDto.MenuItemId;

        await _orderingContext.SaveChangesAsync();

        return new OrderItemReadDto
        {
            Id = id,
            Price = orderItemToUpdate.Price,
            Quantity = orderItemToUpdate.Quantity,
            SpecialInstructions = orderItemToUpdate.SpecialInstructions,
            MenuItemId = orderItemToUpdate.MenuItemId
        };
    }

    public async Task DeleteOrderItem(Guid id)
    {
        var orderItemToDelete = await _orderingContext.OrderItems.FindAsync(id);

        if(orderItemToDelete != null)
        {
            _orderingContext.OrderItems.Remove(orderItemToDelete);
            await _orderingContext.SaveChangesAsync();
        }
    }

}
