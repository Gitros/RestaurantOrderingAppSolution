using Application.Contracts;
using Application.Dtos.OrderItems;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class OrderItemController : BaseApiController
{
    private readonly IOrderItemService _orderItemService;

    public OrderItemController(IOrderItemService orderItemService)
    {
        _orderItemService = orderItemService;
    }

    [HttpPost("{orderId}")]
    public async Task<IActionResult> CreateOrderItem([FromBody] OrderItemCreateDto orderItemCreateDto, Guid orderId) =>
        HandleResult(await _orderItemService.CreateOrderItem(orderItemCreateDto, orderId));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderItem(Guid id) =>
        HandleResult(await _orderItemService.GetOrderItem(id));

    [HttpGet]
    public async Task<IActionResult> GetAllOrderItems() =>
        HandleResult(await _orderItemService.GetAllOrderItems());

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrderItem([FromBody] OrderItemUpdateDto orderItemUpdateDto, Guid id) =>
        HandleResult(await _orderItemService.UpdateOrderItem(orderItemUpdateDto, id));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderItem(Guid id) =>
        HandleResult(await _orderItemService.DeleteOrderItem(id));
}
