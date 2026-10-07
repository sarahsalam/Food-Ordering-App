using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public sealed class OrderItemAddonsQueryHandler
    : IRequestHandler<OrderItemAddonsQuery, (bool Exists, List<OrderItemAddonResponse> Data)>
{
    private readonly IOrderItemAddonService _service;

    public OrderItemAddonsQueryHandler(IOrderItemAddonService service)
    {
        _service = service;
    }

    public Task<(bool Exists, List<OrderItemAddonResponse> Data)> Handle(
        OrderItemAddonsQuery request, CancellationToken cancellationToken) =>
        _service.GetByOrderItemAsync(request.OrderItemId);
}
