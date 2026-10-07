using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public sealed class UpdateCartItemCommandHandler
    : IRequestHandler<UpdateCartItemCommand, (bool Success, string? Error, CartItemResponse? Data)>
{
    private readonly ICartItemService _service;

    public UpdateCartItemCommandHandler(ICartItemService service)
    {
        _service = service;
    }

    public Task<(bool Success, string? Error, CartItemResponse? Data)> Handle(
        UpdateCartItemCommand request, CancellationToken cancellationToken) =>
        _service.UpdateAsync(request.Id, request.Request);
}
