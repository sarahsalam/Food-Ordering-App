using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record DeletePermissionCommand(Guid PermissionId) : IRequest<ServiceResult>;
