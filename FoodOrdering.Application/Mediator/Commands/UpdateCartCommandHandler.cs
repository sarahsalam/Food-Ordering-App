using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public sealed class UpdateCartCommandHandler
    : IRequestHandler<UpdateCartCommand, (bool Success, string? Error, CartResponse? Data)>
{
    private readonly ICartService _service;

    public UpdateCartCommandHandler(ICartService service)
    {
        _service = service;
    }

    public Task<(bool Success, string? Error, CartResponse? Data)> Handle(
        UpdateCartCommand request, CancellationToken cancellationToken) =>
        _service.UpdateAsync(request.Id, request.Request);
}
