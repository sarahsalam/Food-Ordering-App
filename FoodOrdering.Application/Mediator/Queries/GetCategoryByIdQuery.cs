using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Queries;

public sealed record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryResponse?>;
