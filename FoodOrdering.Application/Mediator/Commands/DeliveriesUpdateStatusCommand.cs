using FoodOrdering.Application.DTOs.Requests;
using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record DeliveriesUpdateStatusCommand(Guid Id, ChangeOrderStatusRequest Request, ClaimsPrincipal Actor) : IRequest<ServiceResult>;
