using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public record OrderItemQuery(Guid Id):IRequest<OrderItemResponse?>;
