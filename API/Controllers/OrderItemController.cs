using Application.Contracts;
using Application.Dtos.Common;
using Application.Dtos.OrderItems;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderItemController : ControllerBase
{
    private readonly IOrderItemService _orderItemService;

    public OrderItemController(IOrderItemService orderItemService)
    {
        _orderItemService = orderItemService;
    }

    [HttpPost]
    public async Task<ResultDto<OrderItemReadDto>> CreateOrderItem(OrderItemCreateDto orderItemCreateDto) =>
        await _orderItemService.CreateOrderItem(orderItemCreateDto);

    [HttpGet("{id}")]
    public async Task<ResultDto<OrderItemReadDto>> GetOrderItem(Guid id) =>
        await _orderItemService.GetOrderItem(id);

    [HttpGet]
    public async Task<ResultDto<List<OrderItemReadDto>>> GetAllOrderItems() =>
        await _orderItemService.GetAllOrderItems();

    [HttpPut("{id}")]
    public async Task<ResultDto<OrderItemReadDto>> UpdateOrderItem(OrderItemUpdateDto orderItemUpdateDto, Guid id) =>
        await _orderItemService.UpdateOrderItem(orderItemUpdateDto, id);

    [HttpDelete("{id}")]
    public async Task<ResultDto<bool>> DeleteOrderItem(Guid id) =>
        await _orderItemService.DeleteOrderItem(id);
}
