using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

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
