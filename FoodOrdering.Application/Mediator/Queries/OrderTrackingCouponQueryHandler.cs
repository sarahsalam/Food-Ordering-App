using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public class OrderTrackingCouponQueryHandler : IRequestHandler<OrderTrackingCouponQuery, ServiceResult>
{
    private readonly IOrderTrackingService _service;

    public OrderTrackingCouponQueryHandler(IOrderTrackingService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(OrderTrackingCouponQuery request, CancellationToken cancellationToken)
    {
        return _service.CouponAsync(request.OrderId, request.Actor);
    }
}
