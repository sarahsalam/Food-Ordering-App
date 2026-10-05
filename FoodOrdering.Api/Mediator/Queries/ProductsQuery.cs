using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public record ProductsQuery(Guid CategoryId, ProductQueryRequest Query)
    : IRequest<(bool Exists, List<ProductResponse> Data)>;