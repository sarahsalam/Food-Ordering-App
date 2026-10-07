using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record AssignRolePermissionCommand(Guid RoleId, Guid PermissionId) : IRequest<ServiceResult>;
