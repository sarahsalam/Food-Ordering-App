using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

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
