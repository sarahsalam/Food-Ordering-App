using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public sealed class ProductVariantQueryHandler
    : IRequestHandler<ProductVariantQuery, ProductVariantResponse?>
{
    private readonly IProductVariantService _service;

    public ProductVariantQueryHandler(IProductVariantService service)
    {
        _service = service;
    }

    public Task<ProductVariantResponse?> Handle(ProductVariantQuery request,
        CancellationToken cancellationToken) => _service.GetByIdAsync(request.Id);
}