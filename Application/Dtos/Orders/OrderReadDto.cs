﻿using Domain;

namespace Application.Dtos.Orders;

public class OrderReadDto
{
    public Guid Id { get; set; }
    public DateTime OrderDateTime { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus OrderStatus { get; set; }
}