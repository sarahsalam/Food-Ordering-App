using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public sealed class UpdateProductVariantCommandHandler
    : IRequestHandler<UpdateProductVariantCommand, (bool Success, string? Error, ProductVariantResponse? Data)>
{
    private readonly IProductVariantService _service;

    public UpdateProductVariantCommandHandler(IProductVariantService service)
    {
        _service = service;
    }

    public Task<(bool Success, string? Error, ProductVariantResponse? Data)> Handle(
        UpdateProductVariantCommand request, CancellationToken cancellationToken) =>
        _service.UpdateAsync(request.Id, request.Request);
}
