using Domain;

namespace Application.Dtos.Orders.OrderCreate;

public class OrderCreateDto
{
    public DateTime OrderDateTime { get; set; } = DateTime.UtcNow;
    public OrderType OrderType { get; set; }
    public Guid? TableId { get; set; }
    public string? DeliveryAddress { get; set; }
}
