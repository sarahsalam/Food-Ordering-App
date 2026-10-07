using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public record RestaurantOperationsOrdersQuery(Guid RestaurantId, ClaimsPrincipal Actor) : IRequest<ServiceResult>;
