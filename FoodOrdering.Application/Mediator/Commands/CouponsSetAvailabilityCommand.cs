using FoodOrdering.Application.DTOs.Requests;
using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record CouponsSetAvailabilityCommand(Guid Id, CouponAvailabilityRequest Request, ClaimsPrincipal Actor) : IRequest<ServiceResult>;
