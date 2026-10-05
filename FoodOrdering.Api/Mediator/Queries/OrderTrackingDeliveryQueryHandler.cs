using FoodOrdering.Api.Services;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public class OrderTrackingDeliveryQueryHandler : IRequestHandler<OrderTrackingDeliveryQuery, ServiceResult>
{
    private readonly IOrderTrackingService _service;

    public OrderTrackingDeliveryQueryHandler(IOrderTrackingService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(OrderTrackingDeliveryQuery request, CancellationToken cancellationToken)
    {
        return _service.DeliveryAsync(request.OrderId, request.Actor);
    }
}
