using Application.Contracts;
using Application.Dtos.Orders;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class OrderController(IOrderService orderService) : BaseApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto orderCreateDto) =>
        HandleResult(await orderService.CreateOrder(orderCreateDto));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(Guid id) =>
        HandleResult(await orderService.GetOrder(id));

    [HttpGet]
    public async Task<IActionResult> GetAllOrders() =>
        HandleResult(await orderService.GetAllOrders());

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder([FromBody] OrderUpdateDto orderUpdateDto, Guid id) =>
        HandleResult(await orderService.UpdateOrder(orderUpdateDto, id));

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateOrderStatus([FromBody] OrderStatus newStatus, Guid id) =>
        HandleResult(await orderService.UpdateOrderStatus(newStatus, id));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(Guid id) =>
        HandleResult(await orderService.DeleteOrder(id));
}
