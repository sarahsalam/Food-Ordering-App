using FoodOrdering.Api.Services;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public record RemoveRolePermissionCommand(Guid RoleId, Guid PermissionId) : IRequest<ServiceResult>;
