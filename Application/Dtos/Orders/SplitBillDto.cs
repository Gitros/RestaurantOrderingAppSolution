namespace Application.Dtos.Orders;

public class SplitBillDto
{
    public List<Guid> OrderItemIds { get; set; } = new List<Guid>();
}
