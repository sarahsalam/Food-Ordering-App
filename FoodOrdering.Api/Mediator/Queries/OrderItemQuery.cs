using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public record OrderItemQuery(Guid Id):IRequest<OrderItemResponse?>;
