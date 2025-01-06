using Application.Dtos.OrderItems;

namespace Application.Dtos.Orders;

public class OrderReadDto
{
    public Guid Id { get; set; }
    public DateTime OrderDateTime { get; set; }
    public decimal TotalAmount { get; set; }
    public string OrderStatus { get; set; } = null!;
    public string OrderType { get; set; } = null!;

    public Guid? TableId { get; set; }
    public string? DeliveryAddress { get; set; }

    public List<OrderItemSummaryDto> OrderItems { get; set; } = new List<OrderItemSummaryDto>();
}
