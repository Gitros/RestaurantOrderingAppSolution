using Application.Dtos.Common;
using Application.Dtos.OrderItemIngredients;
using Application.Dtos.OrderItems;

namespace Application.Contracts;

public interface IOrderItemService
{
    Task<ResultDto<OrderItemReadDto>> CreateOrderItem(OrderItemCreateDto orderItemCreateDto, Guid OrderId);
    Task<ResultDto<OrderItemReadDto>> GetOrderItem(Guid id);
    Task<ResultDto<List<OrderItemReadDto>>> GetAllOrderItems();
    Task<ResultDto<OrderItemReadDto>> UpdateOrderItem(OrderItemUpdateDto orderItemUpdateDto, Guid id);
    Task<ResultDto<OrderItemReadDto>> UpdateOrderItemStatus(Guid orderId, Guid orderItemId, OrderItemStatusDto statusDto);
    Task<ResultDto<OrderItemReadDto>> UpdateOrderItemIngredients(Guid orderId, Guid orderItemId, List<OrderItemIngredientAddDto> ingredientDtos);
    Task<ResultDto<OrderItemReadDto>> UpdateOrderItemInstructions(Guid orderId, Guid orderItemId, string specialInstructions);
    Task<ResultDto<bool>> DeleteOrderItem(Guid id);
}
