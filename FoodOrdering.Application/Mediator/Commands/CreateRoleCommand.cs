using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record CreateRoleCommand(string Name) : IRequest<ServiceResult>;
