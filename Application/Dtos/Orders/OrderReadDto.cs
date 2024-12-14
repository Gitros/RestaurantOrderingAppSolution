using Application.Dtos.OrderItems;

namespace Application.Dtos.Orders;

public class OrderReadDto
{
    public Guid Id { get; set; }
    public DateTime OrderDateTime { get; set; }
    public decimal TotalAmount { get; set; }
    public string OrderStatus { get; set; }

    public List<OrderItemSummaryDto> OrderItems { get; set; } 
}
