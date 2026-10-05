using FoodOrdering.Api.Services;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public record RevokePermissionFromRoleCommand(Guid RoleId, Guid PermissionId) : IRequest<ServiceResult>;
