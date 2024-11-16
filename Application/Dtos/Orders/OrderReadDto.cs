using Application.Dtos.OrderItems;
using Domain;

namespace Application.Dtos.Orders;

public class OrderReadDto
{
    public Guid Id { get; set; }
    public DateTime OrderDateTime { get; set; }
    public decimal TotalAmount { get; set; }
    public string OrderStatus { get; set; }

    public List<OrderItemReadDto> OrderItems { get; set; } 

    public Guid TableId { get; set; }
}
