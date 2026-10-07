using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public record CouponsGetQuery(ClaimsPrincipal Actor) : IRequest<ServiceResult>;
