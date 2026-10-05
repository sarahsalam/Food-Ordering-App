using FoodOrdering.Api.Services;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public record AssignRolesToUserCommand(Guid UserId, List<string> Roles) : IRequest<ServiceResult>;
