using Application.Dtos.OrderItems;

namespace Application.Contracts;

public interface IOrderItemService
{
    Task<OrderItemReadDto> CreateOrderItem(OrderItemCreateDto orderItemCreateDto);
    Task<OrderItemReadDto> GetOrderItem(Guid id);
    Task<List<OrderItemReadDto>> GetAllOrderItems();
    Task<OrderItemReadDto> UpdateOrderItem(OrderItemUpdateDto orderItemUpdateDto, Guid id);
    Task DeleteOrderItem(Guid id);
}
