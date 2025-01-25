﻿using Application.Contracts;
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

    [HttpPost("{orderId}/pay")]
    public async Task<IActionResult> PayOrder([FromQuery] PaymentMethod paymentMethod, Guid orderId) =>
        HandleResult(await orderService.PayOrder(paymentMethod, orderId));

    [HttpPost("{orderId}/split")]
    public async Task<IActionResult> SplitBill([FromBody] SplitBillDto splitBillDto, Guid orderId) =>
        HandleResult(await orderService.SplitBill(splitBillDto, orderId));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(Guid id) =>
        HandleResult(await orderService.GetOrder(id));

    [HttpGet]
    public async Task<IActionResult> GetAllOrders([FromQuery] OrderStatus? orderStatus, [FromQuery] PaymentStatus? paymentStatus) =>
        HandleResult(await orderService.GetAllOrders(orderStatus, paymentStatus));

    [HttpPut("{orderId}/apply-discount")]
    public async Task<IActionResult> ApplyOrderDiscount(decimal discount, Guid orderId) =>
        HandleResult(await orderService.ApplyOrderDiscount(discount, orderId));

    [HttpPut("{orderId}/order-items/{orderItemId}/apply-discount")]
    public async Task<IActionResult> ApplyOrderItemDiscount(decimal discount, Guid orderId, Guid orderItemId) =>
        HandleResult(await orderService.ApplyOrderItemDiscount(discount, orderId, orderItemId));

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
