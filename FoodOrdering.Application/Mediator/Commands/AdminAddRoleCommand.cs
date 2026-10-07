using FoodOrdering.Application.DTOs.Requests;
using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record AdminAddRoleCommand(Guid UserId, AssignRoleRequest Request, ClaimsPrincipal Actor) : IRequest<ServiceResult>;
