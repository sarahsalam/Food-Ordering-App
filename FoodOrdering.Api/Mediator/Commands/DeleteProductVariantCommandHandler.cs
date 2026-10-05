using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public sealed class DeleteProductVariantCommandHandler
    : IRequestHandler<DeleteProductVariantCommand, (bool Success, string? Error)>
{
    private readonly IProductVariantService _service;

    public DeleteProductVariantCommandHandler(IProductVariantService service)
    {
        _service = service;
    }

    public Task<(bool Success, string? Error)> Handle(DeleteProductVariantCommand request,
        CancellationToken cancellationToken) => _service.DeleteAsync(request.Id);
}