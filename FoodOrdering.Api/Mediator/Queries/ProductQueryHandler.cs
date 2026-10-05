using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

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