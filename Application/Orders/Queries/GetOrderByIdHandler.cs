using Contracts.Dtos;
using Contracts.Interfaces;
using MediatR;

namespace Application.Orders.Queries;

public class GetOrderByIdHandler : IRequestHandler<GetOrderById, OrderDto>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderByIdHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderDto> Handle(GetOrderById request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderById(request.Id);

        if (order == null) return null;
        return order;
    }
}
