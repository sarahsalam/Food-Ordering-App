using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record DeleteProductVariantCommand(Guid Id):IRequest<(bool Success,string? Error)>;
