using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public class CouponsGetQueryHandler : IRequestHandler<CouponsGetQuery, ServiceResult>
{
    private readonly ICouponsService _service;

    public CouponsGetQueryHandler(ICouponsService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(CouponsGetQuery request, CancellationToken cancellationToken)
    {
        return _service.GetAsync(request.Actor);
    }
}
