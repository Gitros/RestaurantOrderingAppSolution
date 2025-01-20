﻿using Application.Dtos.Common;
using Application.Dtos.OrderItems;
using Application.Dtos.Orders;
using Application.Dtos.Orders.OrderCreate;
using Domain;

namespace Application.Contracts;

public interface IOrderService
{
    Task<ResultDto<OrderReadDto>> CreateDineInOrder(DineInOrderCreateDto dineInOrderDto);
    Task<ResultDto<OrderReadDto>> CreateTakeawayOrder(TakeawayOrderCreateDto takeawayOrderDto);
    Task<ResultDto<OrderReadDto>> CreateDeliveryOrder(DeliveryOrderCreateDto deliveryOrderDto);
    Task<ResultDto<OrderReadDto>> AddOrderItem(OrderItemCreateDto orderItemDto, Guid orderId);
    Task<ResultDto<OrderReadDto>> PayOrder(PaymentMethod paymentMethod, Guid orderId);
    Task<ResultDto<OrderReadDto>> SplitBill(SplitBillDto splitBillDto, Guid orderId);
    Task<ResultDto<OrderReadDto>> GetOrder(Guid id);
    Task<ResultDto<List<OrderReadDto>>> GetAllOrders(OrderStatus? orderStatus, PaymentStatus? paymentStatus);
    Task<ResultDto<OrderReadDto>> ChangeOrderTable(Guid orderId, Guid newTableId);
    Task<ResultDto<OrderReadDto>> UpdateOrderStatus(OrderStatus newStatus, Guid id);
    Task<ResultDto<OrderReadDto>> UpdateOrderType(OrderType newOrderType, OrderUpdateTypeDto updateTypeDto, Guid orderId);
    Task<ResultDto<bool>> DeleteOrder(Guid id);
}