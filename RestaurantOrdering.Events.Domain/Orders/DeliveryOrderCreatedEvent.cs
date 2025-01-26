using Domain;

namespace RestaurantOrdering.Events.Domain.Orders;

public class DeliveryOrderCreatedEvent
{
    public DateTime OrderDateTime { get; set; }
    public string PhoneNumber { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string? AdditionalInstructions { get; set; }

    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
