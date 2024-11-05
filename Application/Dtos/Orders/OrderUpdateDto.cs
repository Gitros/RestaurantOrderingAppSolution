using Domain;

namespace Application.Dtos.Orders;

public class OrderUpdateDto
{
    public decimal TotalAmount { get; set; }
    public OrderStatus OrderStatus { get; set; }

    public Guid TableId { get; set; }
}
