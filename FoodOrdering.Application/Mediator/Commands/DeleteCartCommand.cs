using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record DeleteCartCommand(Guid Id):IRequest<(bool Success,string? Error)>;
