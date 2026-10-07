using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record DeleteAddonCommand(Guid Id):IRequest<(bool Success,string? Error)>;
