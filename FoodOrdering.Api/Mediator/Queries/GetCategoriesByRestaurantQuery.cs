using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Queries;

public sealed record GetCategoriesByRestaurantQuery(
    Guid RestaurantId,
    CategoryQueryRequest Query)
    : IRequest<(bool Exists, List<CategoryResponse> Data)>;
