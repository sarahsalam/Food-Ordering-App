using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;
using FoodOrdering.Api.Mediator.Queries;

namespace FoodOrdering.Api.Mediator.Queries;

public sealed class CartItemQueryHandler
    : IRequestHandler<CartItemQuery, CartItemResponse?>
{
    private readonly ICartItemService _service;

    public CartItemQueryHandler(ICartItemService service)
    {
        _service = service;
    }

    public Task<CartItemResponse?> Handle(CartItemQuery request,
        CancellationToken cancellationToken) => _service.GetByIdAsync(request.Id);
}