using Application.Contracts;
using Application.Dtos.Orders;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<OrderReadDto> CreateOrder(OrderCreateDto orderCreateDto) => await _orderService.CreateOrder(orderCreateDto);
}
