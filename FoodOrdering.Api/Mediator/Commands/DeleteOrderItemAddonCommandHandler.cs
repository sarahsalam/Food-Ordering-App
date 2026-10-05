using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public sealed class DeleteOrderItemAddonCommandHandler
    : IRequestHandler<DeleteOrderItemAddonCommand, (bool Success, string? Error)>
{
    private readonly IOrderItemAddonService _service;

    public DeleteOrderItemAddonCommandHandler(IOrderItemAddonService service)
    {
        _service = service;
    }

    public Task<(bool Success, string? Error)> Handle(
        DeleteOrderItemAddonCommand request,
        CancellationToken cancellationToken) => _service.DeleteAsync(request.Id);
}