using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public sealed record DeleteBranchCommand(Guid Id)
    : IRequest<(bool Success, string? Error)>;
