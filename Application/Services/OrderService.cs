using Application.Contracts;
using Application.Dtos.Common;
using Application.Dtos.OrderItems;
using Application.Dtos.Orders;
using Application.Dtos.Orders.OrderCreate;
using AutoMapper;
using Domain;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Services;

public class OrderService(RestaurantOrderingContext orderingContext, IMapper mapper) : IOrderService
{
    private async Task<List<OrderItem>> PopulateOrderItemsAsync(IEnumerable<OrderItemCreateDto> orderItemDtos, Guid orderId)
    {
        var menuItemIds = orderItemDtos.Select(oi => oi.MenuItemId).Distinct();
        var menuItems = await orderingContext.MenuItems
            .Where(mi => menuItemIds.Contains(mi.Id))
            .ToDictionaryAsync(mi => mi.Id);

        var orderItems = new List<OrderItem>();
        foreach (var itemDto in orderItemDtos)
        {
            if (!menuItems.TryGetValue(itemDto.MenuItemId, out var menuItem))
            {
                throw new KeyNotFoundException($"MenuItem with ID {itemDto.MenuItemId} not found.");
            }

            var orderItem = mapper.Map<OrderItem>(itemDto);
            orderItem.Price = menuItem.Price;
            orderItem.OrderId = orderId;

            orderItems.Add(orderItem);
        }

        return orderItems;
    }
    public async Task<ResultDto<OrderReadDto>> CreateDineInOrder(DineInOrderCreateDto dineInOrderDto)
    {
        try
        {
            var dineInOrder = mapper.Map<Order>(dineInOrderDto);

            var table = await orderingContext.Tables
                .FirstOrDefaultAsync(t => t.Id == dineInOrderDto.TableId);

            if (table == null)
                return ResultDto<OrderReadDto>
                    .Failure("Specified table does not exist.", HttpStatusCode.BadRequest);

            if (table.IsOccupied)
                return ResultDto<OrderReadDto>
                    .Failure("The specified table is already occupied.", HttpStatusCode.Conflict);

            table.IsOccupied = true;


            dineInOrder.OrderItems = await PopulateOrderItemsAsync(dineInOrderDto.OrderItems, dineInOrder.Id);
            dineInOrder.TotalAmount = dineInOrder.OrderItems.Sum(oi => oi.Price * oi.Quantity);

            await orderingContext.Orders.AddAsync(dineInOrder);
            await orderingContext.SaveChangesAsync();

            var createdOrderDto = mapper.Map<OrderReadDto>(dineInOrder);

            return ResultDto<OrderReadDto>
                .Success(createdOrderDto, HttpStatusCode.Created);
        }
        catch (Exception ex)
        {
            return ResultDto<OrderReadDto>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<OrderReadDto>> CreateTakeawayOrder(TakeawayOrderCreateDto takeawayOrderDto)
    {
        try
        {
            var takeawayOrder = mapper.Map<Order>(takeawayOrderDto);

            if(string.IsNullOrWhiteSpace(takeawayOrderDto.PhoneNumber))
                return ResultDto<OrderReadDto>
                    .Failure("Phone number is required for takeaway orders.", HttpStatusCode.BadRequest);

            takeawayOrder.CustomerInformation = mapper.Map<CustomerInformation>(takeawayOrderDto);
            takeawayOrder.CustomerInformation.OrderId = takeawayOrder.Id;

            takeawayOrder.OrderItems = await PopulateOrderItemsAsync(takeawayOrderDto.OrderItems, takeawayOrder.Id);
            takeawayOrder.TotalAmount = takeawayOrder.OrderItems.Sum(oi => oi.Price * oi.Quantity);

            await orderingContext.Orders.AddAsync(takeawayOrder);
            await orderingContext.SaveChangesAsync();

            var orderReadDto = mapper.Map<OrderReadDto>(takeawayOrder);

            return ResultDto<OrderReadDto>
                .Success(orderReadDto, HttpStatusCode.Created);
        }
        catch (Exception ex)
        {
            return ResultDto<OrderReadDto>
                .Failure($"An error occurred: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<ResultDto<OrderReadDto>> CreateDeliveryOrder(DeliveryOrderCreateDto deliveryOrderDto)
    {
        try
        {
            var deliveryOrder = mapper.Map<Order>(deliveryOrderDto);

            deliveryOrder.CustomerInformation = mapper.Map<CustomerInformation>(deliveryOrderDto);
            deliveryOrder.CustomerInformation.OrderId = deliveryOrder.Id;

            deliveryOrder.OrderItems = await PopulateOrderItemsAsync(deliveryOrderDto.OrderItems, deliveryOrder.Id);
            deliveryOrder.TotalAmount = deliveryOrder.OrderItems.Sum(oi => oi.Price * oi.Quantity);

            await orderingContext.Orders.AddAsync(deliveryOrder);
            await orderingContext.SaveChangesAsync();

            var orderReadDto = mapper.Map<OrderReadDto>(deliveryOrder);

            return ResultDto<OrderReadDto>
                .Success(orderReadDto, HttpStatusCode.Created);
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
                .Include(o => o.CustomerInformation)
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