using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public record OrderTrackingDeliveryQuery(Guid OrderId, ClaimsPrincipal Actor) : IRequest<ServiceResult>;
