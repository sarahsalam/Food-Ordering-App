using FoodOrdering.Api.Services;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public record AssignRolePermissionCommand(Guid RoleId, Guid PermissionId) : IRequest<ServiceResult>;
