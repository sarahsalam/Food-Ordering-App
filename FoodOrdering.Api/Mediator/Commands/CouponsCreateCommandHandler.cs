using FoodOrdering.Api.Services;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public class CouponsCreateCommandHandler : IRequestHandler<CouponsCreateCommand, ServiceResult>
{
    private readonly ICouponsService _service;

    public CouponsCreateCommandHandler(ICouponsService service)
    {
        _service = service;
    }

    public Task<ServiceResult> Handle(CouponsCreateCommand request, CancellationToken cancellationToken)
    {
        return _service.CreateAsync(request.Request, request.Actor);
    }
}
