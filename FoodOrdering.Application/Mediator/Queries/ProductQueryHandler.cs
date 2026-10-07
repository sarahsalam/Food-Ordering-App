using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public sealed class ProductQueryHandler
    : IRequestHandler<ProductQuery, ProductResponse?>
{
    private readonly IProductService _service;

    public ProductQueryHandler(IProductService service)
    {
        _service = service;
    }

    public Task<ProductResponse?> Handle(ProductQuery request,
        CancellationToken cancellationToken) => _service.GetByIdAsync(request.Id);
}
