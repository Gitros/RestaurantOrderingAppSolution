using Application.Contracts;
using Application.Dtos.OrderItems;
using Application.Dtos.Orders;
using Application.Dtos.Orders.OrderCreate;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class OrderController(IOrderService orderService) : BaseApiController
{
    [HttpPost("dinein")]
    public async Task<IActionResult> CreateDineInOrder([FromBody] DineInOrderCreateDto dineInOrderDto) =>
    HandleResult(await orderService.CreateDineInOrder(dineInOrderDto));

    [HttpPost("takeaway")]
    public async Task<IActionResult> CreateTakeawayOrder([FromBody] TakeawayOrderCreateDto takeawayOrderDto) =>
        HandleResult(await orderService.CreateTakeawayOrder(takeawayOrderDto));

    [HttpPost("delivery")]
    public async Task<IActionResult> CreateDeliveryOrder([FromBody] DeliveryOrderCreateDto deliveryOrderDto) =>
        HandleResult(await orderService.CreateDeliveryOrder(deliveryOrderDto));

    [HttpPost("{orderId}/item")]
    public async Task<IActionResult> AddOrderItem([FromBody] OrderItemCreateDto orderItemDto, Guid orderId) =>
        HandleResult(await orderService.AddOrderItem(orderItemDto, orderId));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(Guid id) =>
        HandleResult(await orderService.GetOrder(id));

    [HttpGet]
    public async Task<IActionResult> GetAllOrders([FromQuery] OrderStatus? orderStatus, [FromQuery] PaymentStatus? paymentStatus) =>
        HandleResult(await orderService.GetAllOrders(orderStatus, paymentStatus));

    [HttpPut("{orderId}/change-table")]
    public async Task<IActionResult> ChangeOrderTable(Guid orderId, [FromBody] Guid newTableId) =>
        HandleResult(await orderService.ChangeOrderTable(orderId, newTableId));

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateOrderStatus([FromBody] OrderStatus newStatus, Guid id) =>
        HandleResult(await orderService.UpdateOrderStatus(newStatus, id));

    [HttpPut("{orderId}/type")]
    public async Task<IActionResult> UpdateOrderType([FromQuery] OrderType newOrderType, [FromBody] OrderUpdateTypeDto updateTypeDto, Guid orderId) =>
    HandleResult(await orderService.UpdateOrderType(newOrderType, updateTypeDto, orderId));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(Guid id) =>
        HandleResult(await orderService.DeleteOrder(id));
}
