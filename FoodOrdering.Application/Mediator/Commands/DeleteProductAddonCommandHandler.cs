using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public sealed class DeleteProductAddonCommandHandler
    : IRequestHandler<DeleteProductAddonCommand, (bool Success, string? Error)>
{
    private readonly IProductAddonService _service;

    public DeleteProductAddonCommandHandler(IProductAddonService service)
    {
        _service = service;
    }

    public Task<(bool Success, string? Error)> Handle(DeleteProductAddonCommand request,
        CancellationToken cancellationToken) => _service.RemoveAsync(request.ProductId, request.AddonId);
}
