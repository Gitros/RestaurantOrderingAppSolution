using Contracts.Dtos;
using MediatR;

namespace Application.Orders.Queries;

public class GetOrderById : IRequest<OrderDto>
{
    public Guid Id { get; set; }

    public GetOrderById(Guid id)
    {
        Id = id;
    }
}
