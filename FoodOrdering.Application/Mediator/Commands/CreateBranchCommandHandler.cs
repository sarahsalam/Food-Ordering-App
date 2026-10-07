using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public sealed class CreateBranchCommandHandler
    : IRequestHandler<CreateBranchCommand, (bool Success, string? Error, BranchResponse? Data)>
{
    private readonly IRestaurantBranchService _service;

    public CreateBranchCommandHandler(IRestaurantBranchService service)
    {
        _service = service;
    }

    public Task<(bool Success, string? Error, BranchResponse? Data)> Handle(
        CreateBranchCommand request,
        CancellationToken cancellationToken) =>
        _service.CreateAsync(request.RestaurantId, request.OwnerId, request.Request);
}
