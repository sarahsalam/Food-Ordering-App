using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public record OrderItemAddonsQuery(Guid OrderItemId) : IRequest<(bool Exists, List<OrderItemAddonResponse> Data)>;