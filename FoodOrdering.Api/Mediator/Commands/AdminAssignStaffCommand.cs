using System.Security.Claims;
using FoodOrdering.Api.Models.Entities;
using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Services;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public record AdminAssignStaffCommand(Guid RestaurantId, Guid UserId, ClaimsPrincipal Actor) : IRequest<ServiceResult>;
