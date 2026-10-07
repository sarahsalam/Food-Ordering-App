using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public sealed class CartQueryHandler
    : IRequestHandler<CartQuery, CartResponse?>
{
    private readonly ICartService _service;

    public CartQueryHandler(ICartService service)
    {
        _service = service;
    }

    public Task<CartResponse?> Handle(CartQuery request,
        CancellationToken cancellationToken) => _service.GetByIdAsync(request.Id);
}
