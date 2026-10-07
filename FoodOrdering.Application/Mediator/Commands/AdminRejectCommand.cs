using FoodOrdering.Application.DTOs.Requests;
using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record AdminRejectCommand(Guid Id, ReviewRestaurantRequest Request, ClaimsPrincipal Actor) : IRequest<ServiceResult>;
