using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public class PaymentsSubmitToOwnerCommandHandler : IRequestHandler<PaymentsSubmitToOwnerCommand, ServiceResult>
{
    private readonly IPaymentsService _service;

    public PaymentsSubmitToOwnerCommandHandler(IPaymentsService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(PaymentsSubmitToOwnerCommand request, CancellationToken cancellationToken)
    {
        return _service.SubmitToOwnerAsync(request.OrderId, request.Request, request.Actor);
    }
}
