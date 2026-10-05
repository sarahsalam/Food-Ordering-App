using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public sealed record DeleteCategoryCommand(Guid Id) : IRequest<(bool Success, string? Error)>;
