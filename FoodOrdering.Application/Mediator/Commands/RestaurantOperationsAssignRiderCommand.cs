using FoodOrdering.Application.DTOs.Requests;
using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record RestaurantOperationsAssignRiderCommand(Guid Id, AssignRiderRequest Request, ClaimsPrincipal Actor) : IRequest<ServiceResult>;
