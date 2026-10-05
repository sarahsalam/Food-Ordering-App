using FoodOrdering.Api.Models.Responses;
using FoodOrdering.Api.Services.Interfaces;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public sealed class VariantsQueryHandler
    : IRequestHandler<VariantsQuery, (bool Exists, List<ProductVariantResponse> Data)>
{
    private readonly IProductVariantService _service;

    public VariantsQueryHandler(IProductVariantService service)
    {
        _service = service;
    }

    public Task<(bool Exists, List<ProductVariantResponse> Data)> Handle(
        VariantsQuery request, CancellationToken cancellationToken) =>
        _service.GetByProductAsync(request.ProductId, request.Query);
}