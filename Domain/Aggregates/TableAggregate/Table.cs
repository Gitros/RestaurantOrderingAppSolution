using Domain.Aggregates.OrderAggregate;

namespace Domain.Aggregates.TableAggregate;

public class Table
{
    public int TableId { get; set; }
    public string TableNumber { get; set; }
    public int People { get; set; }
    public bool IsOccupied { get; set; }

    public List<Order> Orders { get; set; }
}
