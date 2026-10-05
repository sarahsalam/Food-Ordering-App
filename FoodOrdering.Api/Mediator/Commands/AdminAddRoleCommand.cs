using System.Security.Claims;
using FoodOrdering.Api.Models.Entities;
using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Services;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public record AdminAddRoleCommand(Guid UserId, AssignRoleRequest Request, ClaimsPrincipal Actor) : IRequest<ServiceResult>;
