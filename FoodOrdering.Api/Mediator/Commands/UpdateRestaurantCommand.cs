using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public sealed record UpdateRestaurantCommand(Guid Id, UpdateRestaurantRequest Request)
    : IRequest<(bool Success, string? Error, RestaurantResponse? Data)>;
