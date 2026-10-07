using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public sealed record CreateRestaurantCommand(Guid OwnerId, CreateRestaurantRequest Request)
    : IRequest<(bool Success, string? Error, RestaurantResponse? Data)>;
