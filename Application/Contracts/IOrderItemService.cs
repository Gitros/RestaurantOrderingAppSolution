using Application.Dtos.Common;
using Application.Dtos.OrderItems;

namespace Application.Contracts;

public interface IOrderItemService
{
    Task<ResultDto<OrderItemReadDto>> CreateOrderItem(OrderItemCreateDto orderItemCreateDto, Guid OrderId);
    Task<ResultDto<OrderItemReadDto>> GetOrderItem(Guid id);
    Task<ResultDto<List<OrderItemReadDto>>> GetAllOrderItems();
    Task<ResultDto<OrderItemReadDto>> UpdateOrderItem(OrderItemUpdateDto orderItemUpdateDto, Guid id);
    Task<ResultDto<bool>> DeleteOrderItem(Guid id);
}
