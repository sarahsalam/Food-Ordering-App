using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record AdminRemoveRoleCommand(Guid UserId, Guid RoleId, ClaimsPrincipal Actor) : IRequest<ServiceResult>;
