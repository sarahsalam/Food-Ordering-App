using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public record OrderTrackingCouponQuery(Guid OrderId, ClaimsPrincipal Actor) : IRequest<ServiceResult>;
