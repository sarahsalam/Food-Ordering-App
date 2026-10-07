using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public sealed record GetRestaurantByIdQuery(Guid OwnerId, Guid Id)
    : IRequest<RestaurantResponse?>;
