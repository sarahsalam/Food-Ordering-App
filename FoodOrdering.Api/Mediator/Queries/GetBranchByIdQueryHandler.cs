using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public sealed class GetBranchByIdQueryHandler
    : IRequestHandler<GetBranchByIdQuery, BranchResponse?>
{
    private readonly IRestaurantBranchService _service;

    public GetBranchByIdQueryHandler(IRestaurantBranchService service)
    {
        _service = service;
    }

    public Task<BranchResponse?> Handle(
        GetBranchByIdQuery request,
        CancellationToken cancellationToken) => _service.GetByIdAsync(request.Id);
}
