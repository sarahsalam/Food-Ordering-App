using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public sealed class CreateOrderItemAddonCommandHandler
    : IRequestHandler<CreateOrderItemAddonCommand, (bool Success, string? Error, OrderItemAddonResponse? Data)>
{
    private readonly IOrderItemAddonService _service;

    public CreateOrderItemAddonCommandHandler(IOrderItemAddonService service)
    {
        _service = service;
    }

    public Task<(bool Success, string? Error, OrderItemAddonResponse? Data)> Handle(
        CreateOrderItemAddonCommand request, CancellationToken cancellationToken) =>
        _service.CreateAsync(request.OrderItemId, request.Request);
}
