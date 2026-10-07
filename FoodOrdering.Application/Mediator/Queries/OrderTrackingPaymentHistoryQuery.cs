using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public record OrderTrackingPaymentHistoryQuery(Guid OrderId, ClaimsPrincipal Actor) : IRequest<ServiceResult>;
