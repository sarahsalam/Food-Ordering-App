using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public sealed record DeleteBranchCommand(Guid Id)
    : IRequest<(bool Success, string? Error)>;
