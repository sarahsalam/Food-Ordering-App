using FoodOrdering.Api.Models.Requests;
using FoodOrdering.Api.Models.Responses;
using MediatR;

namespace FoodOrdering.Api.Mediator.Commands;

public record UpdateOrderCommand(Guid Id, UpdateOrderRequest Request)
    : IRequest<(bool Success, string? Error, OrderResponse? Data)>;