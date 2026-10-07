using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public sealed class ProductAddonsQueryHandler
    : IRequestHandler<ProductAddonsQuery, (bool Exists, List<ProductAddonResponse> Data)>
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
