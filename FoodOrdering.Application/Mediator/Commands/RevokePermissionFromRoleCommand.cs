using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record RevokePermissionFromRoleCommand(Guid RoleId, Guid PermissionId) : IRequest<ServiceResult>;
