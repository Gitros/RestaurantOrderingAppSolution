using Application.Dtos.OrderItems;
using Domain;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Orders;

public class OrderCreateDto
{
    public DateTime OrderDateTime { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus OrderStatus { get; set; }

    [MinLength(1, ErrorMessage = "x")]
    public List<OrderItemCreateDto> OrderItems { get; set; }

    public Guid TableId { get; set; }
}
