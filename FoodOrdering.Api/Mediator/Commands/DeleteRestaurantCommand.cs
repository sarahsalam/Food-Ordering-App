using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public sealed record DeleteRestaurantCommand(Guid Id)
    : IRequest<(bool Success, string? Error)>;
