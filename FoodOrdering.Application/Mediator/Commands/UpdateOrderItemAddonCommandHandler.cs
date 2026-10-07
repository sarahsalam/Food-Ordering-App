using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public sealed class UpdateOrderItemAddonCommandHandler
    : IRequestHandler<UpdateOrderItemAddonCommand, (bool Success, string? Error, OrderItemAddonResponse? Data)>
{
    private readonly IOrderItemAddonService _service;

    public UpdateOrderItemAddonCommandHandler(IOrderItemAddonService service)
    {
        _service = service;
    }

    public Task<(bool Success, string? Error, OrderItemAddonResponse? Data)> Handle(
        UpdateOrderItemAddonCommand request, CancellationToken cancellationToken) =>
        _service.UpdateAsync(request.Id, request.Request);
}
