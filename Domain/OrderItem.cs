﻿namespace Domain;

public class OrderItem
{
    public Guid Id { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string SpecialInstructions { get; set; }

    public Guid OrderId { get; set; }
    public Order Order { get; set; }

    public Guid MenuItemId { get; set; }
    public MenuItem MenuItem { get; set; }
}