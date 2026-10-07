using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public record OrderTrackingPaymentQuery(Guid OrderId, ClaimsPrincipal Actor) : IRequest<ServiceResult>;
