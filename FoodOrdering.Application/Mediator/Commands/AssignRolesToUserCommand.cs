using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record AssignRolesToUserCommand(Guid UserId, List<string> Roles) : IRequest<ServiceResult>;
