using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public record AddProductAddonCommand(Guid ProductId, Guid AddonId)
    : IRequest<(bool Success, string? Error, ProductAddonResponse? Data)>;