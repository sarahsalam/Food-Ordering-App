using FoodOrdering.Api.Services;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public class PaymentsCollectCommandHandler : IRequestHandler<PaymentsCollectCommand, ServiceResult>
{
    private readonly IPaymentsService _service;

    public PaymentsCollectCommandHandler(IPaymentsService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(PaymentsCollectCommand request, CancellationToken cancellationToken)
    {
        return _service.CollectAsync(request.OrderId, request.Request, request.Actor);
    }
}
