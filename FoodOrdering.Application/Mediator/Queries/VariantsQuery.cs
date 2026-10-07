using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public record VariantsQuery(Guid ProductId, ProductQueryRequest Query)
    : IRequest<(bool Exists, List<ProductVariantResponse> Data)>;
