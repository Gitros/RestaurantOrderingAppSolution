﻿using Application.Dtos.CustomerInformations;
using Application.Dtos.OrderItems;

namespace Application.Dtos.Orders;

public class OrderReadDto
{
    public Guid Id { get; set; }
    public DateTime OrderDateTime { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal Discount { get; set; }
    public string OrderStatus { get; set; } = null!;
    public string OrderType { get; set; } = null!;
    public string PaymentStatus { get; set; } = null!;

    public Guid? TableId { get; set; }
    public CustomerInformationReadDto? CustomerInformation { get; set; }

    public List<OrderItemSummaryDto> OrderItems { get; set; } = new List<OrderItemSummaryDto>();
}
