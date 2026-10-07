using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public record ProductsQuery(Guid CategoryId, ProductQueryRequest Query)
    : IRequest<(bool Exists, List<ProductResponse> Data)>;
