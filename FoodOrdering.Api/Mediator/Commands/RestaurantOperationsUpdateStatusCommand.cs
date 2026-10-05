using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Services;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public record RestaurantOperationsUpdateStatusCommand(Guid Id, ChangeOrderStatusRequest Request, ClaimsPrincipal Actor) : IRequest<ServiceResult>;
