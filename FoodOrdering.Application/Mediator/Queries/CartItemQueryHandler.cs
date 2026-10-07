using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

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
