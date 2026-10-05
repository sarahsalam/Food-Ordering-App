using FoodOrdering.Api.Services;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public record RevokeRoleFromUserCommand(Guid UserId, Guid RoleId) : IRequest<ServiceResult>;
