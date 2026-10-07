using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record DeleteCartItemCommand(Guid Id):IRequest<(bool Success,string? Error)>;
