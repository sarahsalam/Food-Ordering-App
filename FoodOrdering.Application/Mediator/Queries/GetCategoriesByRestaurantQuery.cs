using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public sealed record GetCategoriesByRestaurantQuery(
    Guid RestaurantId,
    CategoryQueryRequest Query)
    : IRequest<(bool Exists, List<CategoryResponse> Data)>;
