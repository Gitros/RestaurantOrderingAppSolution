﻿using Application.Dtos.OrderItemIngredients;
using Domain;

namespace Application.Dtos.OrderItems;

public class OrderItemUpdateDto
{
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string SpecialInstructions { get; set; }

    public Guid MenuItemId { get; set; }
    public OrderItemStatus OrderItemStatus { get; set; }

    public List<OrderItemIngredientAddDto> Ingredients { get; set; } = new List<OrderItemIngredientAddDto>();
}
