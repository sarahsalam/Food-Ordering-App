using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public sealed record CreateRestaurantCommand(Guid OwnerId, CreateRestaurantRequest Request)
    : IRequest<(bool Success, string? Error, RestaurantResponse? Data)>;
