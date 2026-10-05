using FoodOrdering.Api.Services;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public record DeleteRoleCommand(Guid RoleId) : IRequest<ServiceResult>;
