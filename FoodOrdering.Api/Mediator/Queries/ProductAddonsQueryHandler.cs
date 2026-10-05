using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public sealed class ProductAddonsQueryHandler
    : IRequestHandler<ProductAddonsQuery, (bool Exists, List<ProductAddonResponse>
        Data)>
{
    private readonly IProductAddonService _service;

    public ProductAddonsQueryHandler(IProductAddonService service)
    {
        _service = service;
    }

    public Task<(bool Exists, List<ProductAddonResponse> Data)> Handle(
        ProductAddonsQuery request, CancellationToken cancellationToken) =>
        _service.GetByProductAsync(request.ProductId);
}