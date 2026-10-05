using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public record UpdateOrderItemAddonCommand(Guid Id, UpdateOrderItemAddonRequest Request)
    : IRequest<(bool Success, string? Error, OrderItemAddonResponse? Data)>;