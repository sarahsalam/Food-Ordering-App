using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record CouponsAssignCommand(Guid CouponId, Guid UserId, ClaimsPrincipal Actor) : IRequest<ServiceResult>;
