using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public record OrdersQuery(string? Status,Guid? UserId):IRequest<List<OrderResponse>>;
