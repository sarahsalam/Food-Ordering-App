using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public record MyCouponsGetQuery(ClaimsPrincipal Actor) : IRequest<ServiceResult>;
