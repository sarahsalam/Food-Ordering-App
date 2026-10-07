using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record DeleteProductCommand(Guid Id):IRequest<(bool Success,string? Error)>;
