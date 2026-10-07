using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public record OrderItemAddonQuery(Guid Id):IRequest<OrderItemAddonResponse?>;
