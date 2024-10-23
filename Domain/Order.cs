namespace Domain;

public class Order
{
    public int Id { get; set; }
    public string Table { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new();
    public decimal TotalAmount { get; set; }
}
