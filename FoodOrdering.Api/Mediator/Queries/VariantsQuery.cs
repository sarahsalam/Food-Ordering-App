using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public record VariantsQuery(Guid ProductId, ProductQueryRequest Query)
    : IRequest<(bool Exists, List<ProductVariantResponse> Data)>;