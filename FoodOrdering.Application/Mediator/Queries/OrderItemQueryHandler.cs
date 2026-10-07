using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public sealed class OrderItemQueryHandler
    : IRequestHandler<OrderItemQuery, OrderItemResponse?>
{
    private readonly IOrderItemService _service;

    public OrderItemQueryHandler(IOrderItemService service)
    {
        _service = service;
    }

    public Task<OrderItemResponse?> Handle(OrderItemQuery request,
        CancellationToken cancellationToken) => _service.GetByIdAsync(request.Id);
}
