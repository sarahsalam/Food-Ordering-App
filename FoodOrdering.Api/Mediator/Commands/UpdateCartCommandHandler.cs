using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public sealed class UpdateCartCommandHandler
    : IRequestHandler<UpdateCartCommand, (bool Success, string? Error, CartResponse?
        Data)>
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