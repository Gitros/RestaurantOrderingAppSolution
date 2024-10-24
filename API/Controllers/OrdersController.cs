using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers;

public class OrdersController : BaseApiController
{
    private readonly DataContext _context;

    public OrdersController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Order>>> GetOrders()
    {
        return await _context.Orders.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrder(Guid id)
    {
        return await _context.Orders.FindAsync(id);
    }
}
