using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public sealed class CreateCartCommandHandler
    : IRequestHandler<CreateCartCommand, (bool Success, string? Error, CartResponse? Data)>
{
    private readonly ICartService _service;

    public CreateCartCommandHandler(ICartService service)
    {
        _service = service;
    }

    public Task<(bool Success, string? Error, CartResponse? Data)> Handle(
        CreateCartCommand request, CancellationToken cancellationToken) => _service.CreateAsync(request.Request);
}
