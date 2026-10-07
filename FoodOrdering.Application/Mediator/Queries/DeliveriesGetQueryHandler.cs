using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public class DeliveriesGetQueryHandler : IRequestHandler<DeliveriesGetQuery, ServiceResult>
{
    private readonly IDeliveriesService _service;

    public DeliveriesGetQueryHandler(IDeliveriesService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(DeliveriesGetQuery request, CancellationToken cancellationToken)
    {
        return _service.GetAsync(request.Actor);
    }
}
