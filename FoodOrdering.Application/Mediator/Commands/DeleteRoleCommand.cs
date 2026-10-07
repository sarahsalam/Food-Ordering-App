using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record DeleteRoleCommand(Guid RoleId) : IRequest<ServiceResult>;
