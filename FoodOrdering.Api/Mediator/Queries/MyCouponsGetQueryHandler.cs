using FoodOrdering.Api.Services;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public class MyCouponsGetQueryHandler : IRequestHandler<MyCouponsGetQuery, ServiceResult>
{
    private readonly IMyCouponsService _service;

    public MyCouponsGetQueryHandler(IMyCouponsService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(MyCouponsGetQuery request, CancellationToken cancellationToken)
    {
        return _service.GetAsync(request.Actor);
    }
}
