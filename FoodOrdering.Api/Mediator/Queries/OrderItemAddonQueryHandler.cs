using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public sealed class OrderItemAddonQueryHandler
    : IRequestHandler<OrderItemAddonQuery, OrderItemAddonResponse?>
{
    private readonly IOrderItemAddonService _service;

    public OrderItemAddonQueryHandler(IOrderItemAddonService service)
    {
        _service = service;
    }

    public Task<OrderItemAddonResponse?> Handle(OrderItemAddonQuery request,
        CancellationToken cancellationToken) => _service.GetByIdAsync(request.Id);
}