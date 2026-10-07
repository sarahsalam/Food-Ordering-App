using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record RemoveRolePermissionCommand(Guid RoleId, Guid PermissionId) : IRequest<ServiceResult>;
