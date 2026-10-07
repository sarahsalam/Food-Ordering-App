using FoodOrdering.Application.DTOs.Requests;
using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record CouponsCreateCommand(CreateCouponRequest Request, ClaimsPrincipal Actor) : IRequest<ServiceResult>;
