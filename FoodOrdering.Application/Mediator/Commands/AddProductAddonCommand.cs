using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record AddProductAddonCommand(Guid ProductId, Guid AddonId)
    : IRequest<(bool Success, string? Error, ProductAddonResponse? Data)>;
