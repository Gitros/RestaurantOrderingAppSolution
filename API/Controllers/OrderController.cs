using Application.Contracts;
using Application.Dtos.Common;
using Application.Dtos.Orders;
using Domain;
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
    public async Task<CreateResultDto<OrderReadDto>> CreateOrder(OrderCreateDto orderCreateDto) => 
        await _orderService.CreateOrder(orderCreateDto);

    [HttpGet("{id}")]
    public async Task<OrderReadDto> GetOrder(Guid id) =>
        await _orderService.GetOrder(id);

    [HttpGet]
    public async Task<List<OrderReadDto>> GetAllOrders() =>
        await _orderService.GetAllOrders();

    [HttpPut("{id}")]
    public async Task<OrderReadDto> UpdateOrder([FromBody] OrderUpdateDto orderUpdateDto, Guid id) =>
        await _orderService.UpdateOrder(orderUpdateDto, id);

    [HttpPut("{id}/status")]
    public async Task<OrderReadDto> UpdateOrderStatus(OrderStatus newStatus, Guid id) =>
        await _orderService.UpdateOrderStatus(newStatus, id);

    [HttpDelete("{id}")]
    public async Task DeleteOrder(Guid id) =>
        await _orderService.DeleteOrder(id);
}
