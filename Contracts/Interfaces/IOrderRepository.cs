using Contracts.Dtos;

namespace Contracts.Interfaces;

public interface IOrderRepository
{
    Task<OrderDto> GetOrderById(Guid id);
}
