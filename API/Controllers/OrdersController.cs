using Application.Orders.Queries;
using Domain;
using Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class OrdersController : BaseApiController
{
    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrderById(Guid id)
    {
        var query = new GetOrderById(id);
        var order = await Mediator.Send(query);
        return order != null ? Ok(order) : NotFound();
    }
}
