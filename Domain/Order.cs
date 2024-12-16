using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Order
{
    public Guid Id { get; set; }
    [Required]
    public DateTime OrderDateTime { get; set; } = DateTime.UtcNow;
    public decimal TotalAmount { get; set; }
    public OrderStatus OrderStatus { get; set; }

    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public Guid? TableId { get; set; }
    public Table Table { get; set; }

    public OrderType OrderType { get; set; }

    public string DeliveryAddress { get; set; }
}

public enum OrderStatus
{
    Pending = 0,
    InProgress = 1,
    Cooked = 2,
    Delivered = 3,
    Paid = 4,
    Cancelled = 5,
}

public enum OrderType
{
    DineIn = 0,
    Takeaway = 1,
    Delivery = 2
}