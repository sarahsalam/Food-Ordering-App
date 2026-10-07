using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public sealed class CreateProductVariantCommandHandler
    : IRequestHandler<CreateProductVariantCommand, (bool Success, string? Error, ProductVariantResponse? Data)>
{
    private readonly IProductVariantService _service;

    public CreateProductVariantCommandHandler(IProductVariantService service)
    {
        _service = service;
    }

    public Task<(bool Success, string? Error, ProductVariantResponse? Data)> Handle(
        CreateProductVariantCommand request, CancellationToken cancellationToken) =>
        _service.CreateAsync(request.ProductId, request.Request);
}
