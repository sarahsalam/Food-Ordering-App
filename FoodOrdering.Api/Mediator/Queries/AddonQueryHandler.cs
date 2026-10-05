using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;
using FoodOrdering.Api.Mediator.Queries;

namespace FoodOrdering.Api.Mediator.Queries;

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