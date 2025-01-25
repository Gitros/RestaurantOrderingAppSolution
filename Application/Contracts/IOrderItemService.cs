using Application.Dtos.Common;
using Application.Dtos.OrderItems;
using Application.Dtos.Orders;

namespace Application.Contracts;

public interface IOrderItemService
{
    Task<ResultDto<OrderReadDto>> AddOrderItem(OrderItemCreateDto orderItemDto, Guid orderId);
    Task<ResultDto<OrderReadDto>> AddOrderItems(IEnumerable<OrderItemCreateDto> orderItemDtos, Guid orderId);
    Task<ResultDto<OrderItemReadDto>> GetOrderItem(Guid id);
    Task<ResultDto<List<OrderItemReadDto>>> GetAllOrderItems();
    Task<ResultDto<OrderItemReadDto>> UpdateOrderItem(OrderItemUpdateDto updateDto, Guid orderItemId, Guid orderId);
    Task<ResultDto<bool>> DeleteOrderItem(Guid id);
}
