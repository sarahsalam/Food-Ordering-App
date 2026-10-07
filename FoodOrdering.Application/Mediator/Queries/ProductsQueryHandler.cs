using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public sealed class ProductsQueryHandler
    : IRequestHandler<ProductsQuery, (bool Exists, List<ProductResponse> Data)>
{
    private readonly IProductService _service;

    public ProductsQueryHandler(IProductService service)
    {
        _service = service;
    }

    public Task<(bool Exists, List<ProductResponse> Data)> Handle(
        ProductsQuery request, CancellationToken cancellationToken) =>
        _service.GetByCategoryAsync(request.CategoryId, request.Query);
}
