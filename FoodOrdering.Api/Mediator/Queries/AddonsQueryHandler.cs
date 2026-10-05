using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public sealed class AddonsQueryHandler
    : IRequestHandler<AddonsQuery, List<AddonResponse>>
{
    private readonly IAddonService _service;

    public AddonsQueryHandler(IAddonService service)
    {
        _service = service;
    }

    public Task<List<AddonResponse>> Handle(AddonsQuery request,
        CancellationToken cancellationToken) => _service.GetAsync(request.Query);
}