using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record CreatePermissionCommand(string Name) : IRequest<ServiceResult>;
