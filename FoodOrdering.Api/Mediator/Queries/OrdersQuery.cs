using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public record OrdersQuery(string? Status,Guid? UserId):IRequest<List<OrderResponse>>;
