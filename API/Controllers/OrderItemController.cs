using Application.Contracts;
using Application.Dtos.OrderItems;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class OrderItemController(IOrderItemService orderItemService) : BaseApiController
{
    [HttpPost("{orderId}/item")]
    public async Task<IActionResult> AddOrderItem([FromBody] OrderItemCreateDto orderItemDto, Guid orderId) =>
        HandleResult(await orderItemService.AddOrderItem(orderItemDto, orderId));

    [HttpPost("{orderId}/items")]
    public async Task<IActionResult> AddOrderItems([FromBody] IEnumerable<OrderItemCreateDto> orderItemDtos, Guid orderId) =>
        HandleResult(await orderItemService.AddOrderItems(orderItemDtos, orderId));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderItem(Guid id) =>
        HandleResult(await orderItemService.GetOrderItem(id));

    [HttpGet]
    public async Task<IActionResult> GetAllOrderItems() =>
        HandleResult(await orderItemService.GetAllOrderItems());

    [HttpPut("{orderId}/items/{orderItemId}")]
    public async Task<IActionResult> UpdateOrderItem([FromBody] OrderItemUpdateDto updateDto, Guid orderItemId, Guid orderId) =>
        HandleResult(await orderItemService.UpdateOrderItem(updateDto, orderItemId, orderId));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderItem(Guid id) =>
        HandleResult(await orderItemService.DeleteOrderItem(id));
}
