using FoodOrdering.Api.Services;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public class CouponsUsagesQueryHandler : IRequestHandler<CouponsUsagesQuery, ServiceResult>
{
    private readonly ICouponsService _service;

    public CouponsUsagesQueryHandler(ICouponsService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(CouponsUsagesQuery request, CancellationToken cancellationToken)
    {
        return _service.UsagesAsync(request.CouponId, request.Actor);
    }
}
