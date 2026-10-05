using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

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