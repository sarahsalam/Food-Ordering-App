using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public sealed record GetRestaurantsQuery(Guid OwnerId, RestaurantQueryRequest Query)
    : IRequest<List<RestaurantResponse>>;
