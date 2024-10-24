using Domain.Aggregates.TableAggregate;

namespace Domain.Aggregates.OrderAggregate;

public class Order
{
    public Guid OrderId { get; set; }
    public int TableId { get; set; }
    public DateTime OrderDateTime { get; set; }
    public decimal TotalAmount { get; set; }
    public string OrderStatus { get; set; }
    public List<OrderItem> OrderItems { get; set; }
    public Table Table { get; set; }
}
