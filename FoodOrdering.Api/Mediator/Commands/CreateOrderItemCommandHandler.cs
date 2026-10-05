using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public sealed class CreateOrderItemCommandHandler
    : IRequestHandler<CreateOrderItemCommand, (bool Success, string? Error,
        OrderItemResponse? Data)>
{
    private readonly IOrderItemService _service;

    public CreateOrderItemCommandHandler(IOrderItemService service)
    {
        _service = service;
    }

    public Task<(bool Success, string? Error, OrderItemResponse? Data)> Handle(
        CreateOrderItemCommand request, CancellationToken cancellationToken) =>
        _service.CreateAsync(request.OrderId, request.Request);
}