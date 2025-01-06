using Application.Dtos.OrderItems;

namespace Application.Dtos.Orders.OrderCreate;

public class DineInOrderCreateDto
{
    public DateTime OrderDateTime { get; set; }
    public Guid TableId { get; set; }
    public List<OrderItemCreateDto> OrderItems { get; set; } = new List<OrderItemCreateDto>();
}
