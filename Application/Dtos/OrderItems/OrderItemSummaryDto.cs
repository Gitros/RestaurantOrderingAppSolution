﻿namespace Application.Dtos.OrderItems;

public class OrderItemSummaryDto
{
    public Guid Id { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string SpecialInstructions { get; set; }

    public string OrderItemStatus { get; set; }
}