using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;
using FoodOrdering.Api.Mediator.Queries;

namespace FoodOrdering.Api.Mediator.Queries;

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