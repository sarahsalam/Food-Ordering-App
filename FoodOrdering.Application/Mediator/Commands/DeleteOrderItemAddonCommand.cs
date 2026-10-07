using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record DeleteOrderItemAddonCommand(Guid Id) : IRequest<(bool Success, string? Error)>;
