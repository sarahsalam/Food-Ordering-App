using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record AssignPermissionsToRoleCommand(Guid RoleId, List<string> Permissions) : IRequest<ServiceResult>;
