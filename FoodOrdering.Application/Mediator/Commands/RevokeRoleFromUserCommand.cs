using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record RevokeRoleFromUserCommand(Guid UserId, Guid RoleId) : IRequest<ServiceResult>;
