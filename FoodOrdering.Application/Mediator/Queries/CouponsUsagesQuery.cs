using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public record CouponsUsagesQuery(Guid CouponId, ClaimsPrincipal Actor) : IRequest<ServiceResult>;
