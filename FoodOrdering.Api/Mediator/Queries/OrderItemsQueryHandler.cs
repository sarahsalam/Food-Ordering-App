using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public sealed class OrderItemsQueryHandler
    : IRequestHandler<OrderItemsQuery, (bool Exists, List<OrderItemResponse> Data)>
{
    private readonly IOrderItemService _service;

    public OrderItemsQueryHandler(IOrderItemService service)
    {
        _service = service;
    }

    public Task<(bool Exists, List<OrderItemResponse> Data)> Handle(
        OrderItemsQuery request, CancellationToken cancellationToken) => _service.GetByOrderAsync(request.OrderId);
}