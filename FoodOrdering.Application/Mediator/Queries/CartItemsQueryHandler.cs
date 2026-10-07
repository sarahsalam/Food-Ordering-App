using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public sealed class CartItemsQueryHandler
    : IRequestHandler<CartItemsQuery, (bool Exists, List<CartItemResponse> Data)>
{
    private readonly ICartItemService _service;

    public CartItemsQueryHandler(ICartItemService service)
    {
        _service = service;
    }

    public Task<(bool Exists, List<CartItemResponse> Data)> Handle(
        CartItemsQuery request, CancellationToken cancellationToken) => _service.GetByCartAsync(request.CartId);
}
