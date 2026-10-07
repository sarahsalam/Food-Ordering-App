using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public record AdminRolesQuery(ClaimsPrincipal Actor) : IRequest<ServiceResult>;
