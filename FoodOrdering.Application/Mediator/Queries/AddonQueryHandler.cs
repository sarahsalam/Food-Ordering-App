using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public sealed class AddonQueryHandler
    : IRequestHandler<AddonQuery, AddonResponse?>
{
    private readonly IAddonService _service;

    public AddonQueryHandler(IAddonService service)
    {
        _service = service;
    }

    public Task<AddonResponse?> Handle(AddonQuery request,
        CancellationToken cancellationToken) => _service.GetByIdAsync(request.Id);
}
