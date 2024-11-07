using Application.Dtos.Common;
using Application.Dtos.Orders;
using Domain;

namespace Application.Contracts;

public interface IOrderService
{
    Task<ResultDto<OrderReadDto>> CreateOrder(OrderCreateDto orderCreateDto);
    Task<ResultDto<OrderReadDto>> GetOrder(Guid id);
    Task<ResultDto<List<OrderReadDto>>> GetAllOrders();
    Task<ResultDto<OrderReadDto>> UpdateOrder(OrderUpdateDto orderUpdateDto, Guid id);
    Task<ResultDto<OrderReadDto>> UpdateOrderStatus(OrderStatus newStatus, Guid id);
    Task<ResultDto<bool>> DeleteOrder(Guid id);
}
