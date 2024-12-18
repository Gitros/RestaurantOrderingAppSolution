using Application.Contracts;
using Application.Dtos.OrderItemIngredients;
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

    [HttpPut("{orderId}/items/{orderItemId}/status")]
    public async Task<IActionResult> UpdateOrderItemStatus(Guid orderId, Guid orderItemId, [FromBody] OrderItemStatusDto statusDto) =>
    HandleResult(await _orderItemService.UpdateOrderItemStatus(orderId, orderItemId, statusDto));

    [HttpPut("{orderId}/items/{orderItemId}/ingredients")]
    public async Task<IActionResult> UpdateOrderItemIngredients(Guid orderId, Guid orderItemId, [FromBody] List<OrderItemIngredientAddDto> ingredientDtos) =>
    HandleResult(await _orderItemService.UpdateOrderItemIngredients(orderId, orderItemId, ingredientDtos));

    [HttpPut("{orderId}/items/{orderItemId}/instructions")]
    public async Task<IActionResult> UpdateOrderItemInstructions(Guid orderId, Guid orderItemId, [FromBody] string specialInstructions) =>
    HandleResult(await _orderItemService.UpdateOrderItemInstructions(orderId, orderItemId, specialInstructions));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderItem(Guid id) =>
        HandleResult(await _orderItemService.DeleteOrderItem(id));
}
