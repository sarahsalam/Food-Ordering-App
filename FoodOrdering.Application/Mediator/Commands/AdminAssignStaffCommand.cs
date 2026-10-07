using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record AdminAssignStaffCommand(Guid RestaurantId, Guid UserId, ClaimsPrincipal Actor) : IRequest<ServiceResult>;
