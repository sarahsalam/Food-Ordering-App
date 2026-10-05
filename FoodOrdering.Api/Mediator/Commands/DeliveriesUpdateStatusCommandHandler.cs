using FoodOrdering.Api.Services;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public class DeliveriesUpdateStatusCommandHandler : IRequestHandler<DeliveriesUpdateStatusCommand, ServiceResult>
{
    private readonly IDeliveriesService _service;

    public DeliveriesUpdateStatusCommandHandler(IDeliveriesService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(DeliveriesUpdateStatusCommand request, CancellationToken cancellationToken)
    {
        return _service.UpdateStatusAsync(request.Id, request.Request, request.Actor);
    }
}
