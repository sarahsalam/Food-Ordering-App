using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public sealed record DeleteRestaurantCommand(Guid Id)
    : IRequest<(bool Success, string? Error)>;
