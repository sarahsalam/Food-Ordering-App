using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record DeleteProductAddonCommand(Guid ProductId,Guid AddonId):IRequest<(bool Success,string? Error)>;
