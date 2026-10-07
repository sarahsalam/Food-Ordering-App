using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public sealed record DeleteCategoryCommand(Guid Id) : IRequest<(bool Success, string? Error)>;
