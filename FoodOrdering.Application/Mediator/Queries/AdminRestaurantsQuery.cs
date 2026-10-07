using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public record AdminRestaurantsQuery(RestaurantApprovalStatus ApprovalStatus, ClaimsPrincipal Actor) : IRequest<ServiceResult>;
