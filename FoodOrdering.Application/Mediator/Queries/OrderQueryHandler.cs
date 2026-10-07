using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public sealed class OrderQueryHandler
    : IRequestHandler<OrderQuery, OrderResponse?>
{
    private readonly IOrderService _service;

    public OrderQueryHandler(IOrderService service)
    {
        _service = service;
    }

    public Task<OrderResponse?> Handle(OrderQuery request,
        CancellationToken cancellationToken) => _service.GetByIdAsync(request.Id);
}
