using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public sealed class CreateCartItemCommandHandler
    : IRequestHandler<CreateCartItemCommand, (bool Success, string? Error,
        CartItemResponse? Data)>
{
    private readonly ICartItemService _service;

    public CreateCartItemCommandHandler(ICartItemService service)
    {
        _service = service;
    }

    public Task<(bool Success, string? Error, CartItemResponse? Data)> Handle(
        CreateCartItemCommand request, CancellationToken cancellationToken) =>
        _service.CreateAsync(request.CartId, request.Request);
}