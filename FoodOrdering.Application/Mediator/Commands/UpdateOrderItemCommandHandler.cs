using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public sealed class UpdateOrderItemCommandHandler
    : IRequestHandler<UpdateOrderItemCommand, (bool Success, string? Error, OrderItemResponse? Data)>
{
    private readonly IOrderItemService _service;

    public UpdateOrderItemCommandHandler(IOrderItemService service)
    {
        _service = service;
    }

    public Task<(bool Success, string? Error, OrderItemResponse? Data)> Handle(
        UpdateOrderItemCommand request, CancellationToken cancellationToken) =>
        _service.UpdateAsync(request.Id, request.Request);
}
