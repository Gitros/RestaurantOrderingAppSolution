using Application.Dtos.OrderItems;
using Domain;

namespace Application.Dtos.Orders;

public class OrderCreateDto
{
    public DateTime OrderDateTime { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus OrderStatus { get; set; }

    public List<OrderItemCreateDto> OrderItems { get; set; }

    public Guid TableId { get; set; }
}
