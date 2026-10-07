using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public sealed class DeleteBranchCommandHandler
    : IRequestHandler<DeleteBranchCommand, (bool Success, string? Error)>
{
    private readonly IRestaurantBranchService _service;

    public DeleteBranchCommandHandler(IRestaurantBranchService service)
    {
        _service = service;
    }

    public Task<(bool Success, string? Error)> Handle(
        DeleteBranchCommand request,
        CancellationToken cancellationToken) => _service.DeleteAsync(request.Id);
}
