using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public sealed class GetBranchesByRestaurantQueryHandler
    : IRequestHandler<GetBranchesByRestaurantQuery, (bool Exists, List<BranchResponse> Data)>
{
    private readonly IRestaurantBranchService _service;

    public GetBranchesByRestaurantQueryHandler(IRestaurantBranchService service)
    {
        _service = service;
    }

    public Task<(bool Exists, List<BranchResponse> Data)> Handle(
        GetBranchesByRestaurantQuery request,
        CancellationToken cancellationToken) =>
        _service.GetByRestaurantIdAsync(request.RestaurantId, request.Query);
}
