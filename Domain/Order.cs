namespace Domain;

public class Order
{
    public Guid Id { get; set; }
    public DateTime OrderDateTime { get; set; } = DateTime.UtcNow;
    public decimal TotalAmount { get; set; } = 0;

    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
    public required OrderType OrderType { get; set; }

    public PaymentMethod? PaymentMethod { get; set; }

    public List<OrderItem> OrderItems { get; set; } = new();

    public Guid? TableId { get; set; }
    public Table? Table { get; set; }

    public Guid? CustomerInformationId { get; set; }
    public CustomerInformation? CustomerInformation { get; set; }
}

public enum OrderStatus
{
    Pending,
    InProgress,
    Cooked,
    Delivered,
    Paid,
    Cancelled
}

public enum OrderType
{
    DineIn,
    Takeaway,
    Delivery
}

public enum PaymentMethod
{
    Cash,
    Card
}