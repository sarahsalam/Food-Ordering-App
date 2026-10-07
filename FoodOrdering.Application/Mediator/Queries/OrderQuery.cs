using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public record OrderQuery(Guid Id):IRequest<OrderResponse?>;
