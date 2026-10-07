using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public sealed class UpdateOrderCommandHandler
    : IRequestHandler<UpdateOrderCommand, (bool Success, string? Error, OrderResponse? Data)>
{
    private readonly IOrderService _service;

    public UpdateOrderCommandHandler(IOrderService service)
    {
        _service = service;
    }

    public Task<(bool Success, string? Error, OrderResponse? Data)> Handle(
        UpdateOrderCommand request, CancellationToken cancellationToken) =>
        _service.UpdateAsync(request.Id, request.Request);
}
