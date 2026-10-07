using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public class OrderTrackingPaymentQueryHandler : IRequestHandler<OrderTrackingPaymentQuery, ServiceResult>
{
    private readonly IOrderTrackingService _service;

    public OrderTrackingPaymentQueryHandler(IOrderTrackingService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(OrderTrackingPaymentQuery request, CancellationToken cancellationToken)
    {
        return _service.PaymentAsync(request.OrderId, request.Actor);
    }
}
