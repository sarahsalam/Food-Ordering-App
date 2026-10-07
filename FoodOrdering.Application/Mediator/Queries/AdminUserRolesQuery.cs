using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public record AdminUserRolesQuery(Guid UserId, ClaimsPrincipal Actor) : IRequest<ServiceResult>;
