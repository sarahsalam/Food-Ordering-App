using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public sealed class DeleteProductCommandHandler
    : IRequestHandler<DeleteProductCommand, (bool Success, string? Error)>
{
    private readonly IProductService _service;

    public DeleteProductCommandHandler(IProductService service)
    {
        _service = service;
    }

    public Task<(bool Success, string? Error)>
        Handle(DeleteProductCommand request, CancellationToken cancellationToken) => _service.DeleteAsync(request.Id);
}
