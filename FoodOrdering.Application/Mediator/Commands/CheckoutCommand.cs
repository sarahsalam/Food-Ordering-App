using FoodOrdering.Application.DTOs.Requests;
using FoodOrdering.Application.DTOs.Responses;
using MediatR;

namespace FoodOrdering.Application.Mediator.Commands;

public record CheckoutCommand(Guid UserId, CheckoutRequest Request)
    : IRequest<(bool Success, string? Error, OrderResponse? Data)>;
