using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

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
