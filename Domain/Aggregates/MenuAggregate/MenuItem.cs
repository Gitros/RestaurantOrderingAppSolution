﻿namespace Domain.Aggregates.MenuAggregate;

public class MenuItem
{
    public int MenuItemId { get; set; }
    public int MenuId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    public Menu Menu { get; set; }
}