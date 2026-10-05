using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public sealed record GetRestaurantByIdQuery(Guid OwnerId, Guid Id)
    : IRequest<RestaurantResponse?>;
