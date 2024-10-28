using Application.Dtos.Orders;

namespace Application.Contracts;

public interface IOrderService
{
    Task<OrderReadDto> CreateOrder(OrderCreateDto orderCreateDto);
    Task<OrderReadDto> UpdateOrder(OrderUpdateDto orderUpdateDto);
    Task DeleteOrder(Guid id);
    Task<OrderReadDto> GetOrder(Guid id);
    Task<List<OrderReadDto>> GetAllOrders();
}
