using Application.Dtos.Common;
using Application.Dtos.OrderItems;

namespace Application.Contracts;

public interface IOrderItemService
{
    Task<ResultDto<OrderItemReadDto>> CreateOrderItem(OrderItemCreateDto orderItemCreateDto, Guid orderId);
    Task<ResultDto<OrderItemReadDto>> GetOrderItem(Guid id);
    Task<ResultDto<List<OrderItemReadDto>>> GetAllOrderItems();
    Task<ResultDto<OrderItemReadDto>> UpdateOrderItem(OrderItemUpdateDto updateDto, Guid orderItemId, Guid orderId);
    Task<ResultDto<OrderItemReadDto>> UpdateOrderItemStatus(OrderItemStatusDto statusDto, Guid orderId, Guid orderItemId);
    Task<ResultDto<bool>> DeleteOrderItem(Guid id);
}
