using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public sealed class DeleteRestaurantCommandHandler
    : IRequestHandler<DeleteRestaurantCommand, (bool Success, string? Error)>
{
    private readonly IRestaurantService _service;

    public DeleteRestaurantCommandHandler(IRestaurantService service)
    {
        _service = service;
    }

    public Task<(bool Success, string? Error)> Handle(
        DeleteRestaurantCommand request,
        CancellationToken cancellationToken) => _service.DeleteAsync(request.Id);
}
