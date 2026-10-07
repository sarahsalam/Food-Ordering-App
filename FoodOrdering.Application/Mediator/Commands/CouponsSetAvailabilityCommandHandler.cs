using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public class CouponsSetAvailabilityCommandHandler : IRequestHandler<CouponsSetAvailabilityCommand, ServiceResult>
{
    private readonly ICouponsService _service;

    public CouponsSetAvailabilityCommandHandler(ICouponsService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(CouponsSetAvailabilityCommand request, CancellationToken cancellationToken)
    {
        return _service.SetAvailabilityAsync(request.Id, request.Request, request.Actor);
    }
}
