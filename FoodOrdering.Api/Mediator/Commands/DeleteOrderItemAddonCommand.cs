using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public record DeleteOrderItemAddonCommand(Guid Id) : IRequest<(bool Success, string? Error)>;