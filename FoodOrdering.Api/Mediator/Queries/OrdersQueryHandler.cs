using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public sealed class OrdersQueryHandler
    : IRequestHandler<OrdersQuery, List<OrderResponse>>
{
    private readonly IOrderService _service;

    public OrdersQueryHandler(IOrderService service)
    {
        _service = service;
    }

    public Task<List<OrderResponse>> Handle(OrdersQuery request,
        CancellationToken cancellationToken) => _service.GetAsync(request.Status, request.UserId);
}