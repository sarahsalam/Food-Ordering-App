using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public sealed class UpdateOrderItemCommandHandler
    : IRequestHandler<UpdateOrderItemCommand, (bool Success, string? Error,
        OrderItemResponse? Data)>
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