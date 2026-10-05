using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public sealed record UpdateCategoryCommand(
    Guid Id,
    UpdateCategoryRequest Request)
    : IRequest<(bool Success, string? Error, CategoryResponse? Data)>;