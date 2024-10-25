using Contracts.Dtos;
using Contracts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly DataContext _context;

    public OrderRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<OrderDto> GetOrderById(Guid id)
    {
        var order = await _context.Orders
             .Include(o => o.OrderItems)
             .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null) return null;

        return new OrderDto
        {
            Id = order.Id,
            OrderDateTime = order.OrderDateTime,
            TotalAmount = order.TotalAmount,
        };
    }
}
