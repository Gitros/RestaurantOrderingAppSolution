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
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto orderCreateDto)
    {
        var result = await _orderService.CreateOrder(orderCreateDto);

        if (result.IsSuccess)
            return StatusCode((int)result.HttpStatusCode, result.Data);

        return StatusCode((int)result.HttpStatusCode, result.ErrorMessage);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(Guid id)
    {
        var result = await _orderService.GetOrder(id);

        if (result.IsSuccess)
            return Ok(result.Data);

        return StatusCode((int)result.HttpStatusCode, result.ErrorMessage);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        var result = await _orderService.GetAllOrders();

        if (result.IsSuccess)
            return Ok(result.Data);

        return StatusCode((int)result.HttpStatusCode, result.ErrorMessage);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder([FromBody] OrderUpdateDto orderUpdateDto, Guid id)
    {
        var result = await _orderService.UpdateOrder(orderUpdateDto, id);

        if (result.IsSuccess)
            return Ok(result.Data);

        return StatusCode((int)result.HttpStatusCode, result.ErrorMessage);
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateOrderStatus([FromBody] OrderStatus newStatus, Guid id)
    {
        var result = await _orderService.UpdateOrderStatus(newStatus, id);

        if (result.IsSuccess)
            return Ok(result.Data);

        return StatusCode((int)result.HttpStatusCode, result.ErrorMessage);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        var result = await _orderService.DeleteOrder(id);

        if (result.IsSuccess)
            return StatusCode((int)result.HttpStatusCode, result.Data);

        return StatusCode((int)result.HttpStatusCode, result.ErrorMessage);
    }
}
