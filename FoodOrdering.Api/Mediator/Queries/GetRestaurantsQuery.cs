using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public sealed record GetRestaurantsQuery(Guid OwnerId, RestaurantQueryRequest Query)
    : IRequest<List<RestaurantResponse>>;
