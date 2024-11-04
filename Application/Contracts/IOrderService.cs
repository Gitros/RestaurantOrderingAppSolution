using Application.Dtos.Common;
using Application.Dtos.Orders;
using Domain;

namespace Application.Contracts;

public interface IOrderService
{
    Task<CreateResultDto<OrderReadDto>> CreateOrder(OrderCreateDto orderCreateDto);
    Task<OrderReadDto> GetOrder(Guid id);
    Task<List<OrderReadDto>> GetAllOrders();
    Task<OrderReadDto> UpdateOrder(OrderUpdateDto orderUpdateDto, Guid id);
    Task<OrderReadDto> UpdateOrderStatus(OrderStatus newStatus,Guid id);
    Task DeleteOrder(Guid id);
}
