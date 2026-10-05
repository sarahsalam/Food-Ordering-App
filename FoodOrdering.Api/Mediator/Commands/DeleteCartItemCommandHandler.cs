using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public sealed class DeleteCartItemCommandHandler
    : IRequestHandler<DeleteCartItemCommand, (bool Success, string? Error)>
{
    private readonly ICartItemService _service;

    public DeleteCartItemCommandHandler(ICartItemService service)
    {
        _service = service;
    }

    public Task<(bool Success, string? Error)> Handle(DeleteCartItemCommand request,
        CancellationToken cancellationToken) => _service.DeleteAsync(request.Id);
}