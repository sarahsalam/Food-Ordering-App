using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public record OrderQuery(Guid Id):IRequest<OrderResponse?>;
