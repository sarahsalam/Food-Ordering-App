using System.Security.Claims;
using FoodOrdering.Api.Models.Entities;
using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Services;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public record AdminRestaurantsQuery(RestaurantApprovalStatus ApprovalStatus, ClaimsPrincipal Actor) : IRequest<ServiceResult>;
