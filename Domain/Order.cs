namespace Domain;

public class Order
{
    public Guid Id { get; set; }
    public DateTime OrderDateTime { get; set; }
    public decimal TotalAmount { get; set; }

    public OrderStatus OrderStatus { get; set; }
    public OrderType OrderType { get; set; }

    public PaymentMethod PaymentMethod { get; set; }

    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public Guid? TableId { get; set; }
    public Table Table { get; set; }

    public Guid? DeliveryInformationId { get; set; }
    public DeliveryInformation DeliveryInformation { get; set; }

    public Guid? TakeawayInformationId { get; set; }
    public TakeawayInformation TakeawayInformation { get; set; }
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