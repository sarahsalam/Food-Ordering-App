using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public sealed class OrderItemAddonsQueryHandler
    : IRequestHandler<OrderItemAddonsQuery, (bool Exists, List<OrderItemAddonResponse>
        Data)>
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