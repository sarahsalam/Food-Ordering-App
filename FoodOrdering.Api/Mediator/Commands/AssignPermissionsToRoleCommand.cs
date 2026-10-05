using FoodOrdering.Api.Services;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public record AssignPermissionsToRoleCommand(Guid RoleId, List<string> Permissions) : IRequest<ServiceResult>;
