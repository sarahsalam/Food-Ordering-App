using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public record ProductAddonsQuery(Guid ProductId):IRequest<(bool Exists,List<ProductAddonResponse> Data)>;
