using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public sealed record CreateCategoryCommand(
    Guid RestaurantId,
    CreateCategoryRequest Request)
    : IRequest<(bool Success, string? Error, CategoryResponse? Data)>;
