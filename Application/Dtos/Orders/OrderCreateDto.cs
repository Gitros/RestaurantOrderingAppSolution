using Domain;

namespace Application.Dtos.Orders;

public class OrderCreateDto
{
    public DateTime OrderDateTime { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus OrderStatus { get; set; }
}
