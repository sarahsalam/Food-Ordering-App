using FoodOrdering.Api.Services;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public class OrderTrackingPaymentHistoryQueryHandler : IRequestHandler<OrderTrackingPaymentHistoryQuery, ServiceResult>
{
    private readonly IOrderTrackingService _service;

    public OrderTrackingPaymentHistoryQueryHandler(IOrderTrackingService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(OrderTrackingPaymentHistoryQuery request, CancellationToken cancellationToken)
    {
        return _service.PaymentHistoryAsync(request.OrderId, request.Actor);
    }
}
