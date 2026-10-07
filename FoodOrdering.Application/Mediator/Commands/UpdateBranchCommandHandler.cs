using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public sealed class UpdateBranchCommandHandler
    : IRequestHandler<UpdateBranchCommand, (bool Success, string? Error, BranchResponse? Data)>
{
    private readonly IRestaurantBranchService _service;

    public UpdateBranchCommandHandler(IRestaurantBranchService service)
    {
        _service = service;
    }

    public Task<(bool Success, string? Error, BranchResponse? Data)> Handle(
        UpdateBranchCommand request,
        CancellationToken cancellationToken) =>
        _service.UpdateAsync(request.Id, request.Request);
}
