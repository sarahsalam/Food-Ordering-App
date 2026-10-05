using FoodOrdering.Api.Services;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public class OrderTrackingHistoryQueryHandler : IRequestHandler<OrderTrackingHistoryQuery, ServiceResult>
{
    private readonly IOrderTrackingService _service;

    public OrderTrackingHistoryQueryHandler(IOrderTrackingService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(OrderTrackingHistoryQuery request, CancellationToken cancellationToken)
    {
        return _service.HistoryAsync(request.OrderId, request.Actor);
    }
}
