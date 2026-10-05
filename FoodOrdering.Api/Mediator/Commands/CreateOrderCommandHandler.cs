using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public sealed class CreateOrderCommandHandler
    : IRequestHandler<CreateOrderCommand, (bool Success, string? Error, OrderResponse?
        Data)>
{
    private readonly IOrderService _service;

    public CreateOrderCommandHandler(IOrderService service)
    {
        _service = service;
    }

    public Task<(bool Success, string? Error, OrderResponse? Data)> Handle(
        CreateOrderCommand request, CancellationToken cancellationToken) => _service.CreateAsync(request.Request);
}