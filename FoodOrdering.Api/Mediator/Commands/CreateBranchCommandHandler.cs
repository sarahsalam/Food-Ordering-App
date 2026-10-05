using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

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
