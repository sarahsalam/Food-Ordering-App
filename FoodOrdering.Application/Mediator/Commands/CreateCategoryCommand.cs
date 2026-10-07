using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public sealed record CreateCategoryCommand(
    Guid RestaurantId,
    CreateCategoryRequest Request)
    : IRequest<(bool Success, string? Error, CategoryResponse? Data)>;
