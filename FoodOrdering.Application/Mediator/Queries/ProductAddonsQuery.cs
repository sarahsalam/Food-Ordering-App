using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public record ProductAddonsQuery(Guid ProductId):IRequest<(bool Exists, List<ProductAddonResponse> Data)>;
