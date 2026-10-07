using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public record OrderTrackingHistoryQuery(Guid OrderId, ClaimsPrincipal Actor) : IRequest<ServiceResult>;
